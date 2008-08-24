using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using Ruby;
using Ruby.Builtins;

namespace IronRubyMvcLibrary {
    internal class RubyControllerActionInvoker : ControllerActionInvoker {

        public string Controller {
            get;
            set;
        }

        private Func<object> _action;

        public RubyControllerActionInvoker(ControllerContext context)
            : base(context) {
        }

        protected override MethodInfo FindActionMethod(string actionName, IDictionary<string, object> values) {

            // for now limit action name to alphanumeric characters
            if (!Regex.IsMatch(actionName, @"^(\w)+$")) {
                return null;
            }

            var runtime = IronRuby.CreateRuntime();
            var rubyengine = IronRuby.GetEngine(runtime);
            var ctx = IronRuby.GetExecutionContext(runtime);

            // add references (mscorlib, System, Mvc, and RubyController) + other headers
            Type[] coreTypes = new Type[] { typeof(object), typeof(Uri), typeof(Controller), typeof(RubyController) };
            foreach (Type t in coreTypes) {
                var referenceSource = rubyengine.CreateScriptSourceFromString(String.Format("require '{0}'", t.Assembly.FullName));
                referenceSource.Execute();
            }
            rubyengine.CreateScriptSourceFromString("Controller = IronRubyMvcLibrary::RubyController").Execute();

            // add Controllers + Models paths
            string controllersDir = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Controllers");
            string modelsDir = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Models");
            ctx.Loader.SetLoadPaths(controllersDir, modelsDir);

            // inject controller code
            string fileName = String.Format(@"~\Controllers\{0}.rb", Controller);
            if (!HostingEnvironment.VirtualPathProvider.FileExists(fileName)) {
                return null;
            }

            var file = HostingEnvironment.VirtualPathProvider.GetFile(fileName);
            using (Stream stream = file.Open()) {
                using (TextReader reader = new StreamReader(stream)) {
                    var allScript = reader.ReadToEnd();
                    var source = rubyengine.CreateScriptSourceFromString(allScript);
                    source.Execute();
                }
            }

            ctx.DefineReadOnlyGlobalVariable("controller_context", ControllerContext);
            ctx.DefineReadOnlyGlobalVariable("script_runtime", runtime);

            string controllerRubyClassName = runtime.Globals.VariableNames.SingleOrDefault(name => String.Equals(name, Controller, StringComparison.OrdinalIgnoreCase));
            if (String.IsNullOrEmpty(controllerRubyClassName)) {
                // controller not found
                return null;
            }

            RubyClass controllerRubyClass = runtime.Globals.GetVariable<RubyClass>(controllerRubyClassName);
            string controllerRubyMethodName = null;
            controllerRubyClass.EnumerateMethods((_, symbolId, __) => {
                if (String.Equals(symbolId.ToString(), actionName, StringComparison.OrdinalIgnoreCase)) {
                    controllerRubyMethodName = symbolId.ToString();
                    return true;
                }
                return false;
            });

            if (String.IsNullOrEmpty(controllerRubyMethodName)) {
                // action not found
                return null;
            }

            //Instantiate controller.
            string actionScript = @"$controller = {0}.new
$controller.set_ControllerContext $controller_context
$controller.set_ScriptRuntime $script_runtime
$controller.method :{1}";
            
            // get explicit reference to action method object
            var code = String.Format(actionScript, controllerRubyClassName, controllerRubyMethodName);
            object action = rubyengine.CreateScriptSourceFromString(code).Execute();
            _action = () => rubyengine.Operations.Call(action);

            return RubyController.InvokeActionMethod;
        }

        protected override object GetParameterValue(ParameterInfo parameterInfo, IDictionary<string, object> values) {
            if (parameterInfo.Name == "__action") {
                return _action;
            }
            return base.GetParameterValue(parameterInfo, values);
        }

        private static string PascalCaseIt(string s) {
            
            return s[0].ToString().ToUpper() + s.Substring(1);
        }
    }
}
