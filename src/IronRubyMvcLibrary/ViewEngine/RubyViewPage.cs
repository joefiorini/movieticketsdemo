using System;
using System.IO;
using System.Web;
using Ruby;
using System.Web.Mvc;
using Microsoft.Scripting.Hosting;
using Ruby.Runtime;
using IronRubyMvcLibrary.Helpers;

namespace IronRubyViewEngine
{
	public class RubyViewPage
	{
        internal class Container : IViewDataContainer {
            private ViewDataDictionary _viewData;
            internal Container(ViewDataDictionary viewData) {
                _viewData = viewData;
            }
            public ViewDataDictionary ViewData {
                get {
                    return _viewData;
                }
                set {
                    _viewData = value;
                }
            }
        }

		string contents;
		
        public RubyViewPage(TextReader reader)
        {
            this.contents = reader.ReadToEnd();
        }

		public void RenderView(ViewContext context)
		{
            object srt;
            ScriptRuntime runtime = null;
            if (context.ViewData.TryGetValue("__scriptRuntime", out srt)) {
                runtime = (srt as ScriptRuntime);
            }
            if (runtime == null) {
                runtime = IronRuby.CreateRuntime();
            }
            ScriptEngine rubyengine = IronRuby.GetEngine(runtime);
            RubyExecutionContext ctx = IronRuby.GetExecutionContext(runtime);

            ctx.DefineReadOnlyGlobalVariable("view_data", context.ViewData);
            ctx.DefineReadOnlyGlobalVariable("model", context.ViewData.Model);
            ctx.DefineReadOnlyGlobalVariable("context", context);
            ctx.DefineReadOnlyGlobalVariable("response", context.HttpContext.Response);
            ctx.DefineReadOnlyGlobalVariable("html", new RubyHtmlHelper(context, new Container(context.ViewData)));
            ctx.DefineReadOnlyGlobalVariable("url", new RubyUrlHelper(context));
            ctx.DefineReadOnlyGlobalVariable("ajax", new AjaxHelper(context));
			
			RubyTemplate template = new RubyTemplate(this.contents);
            template.AddRequire("System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
            template.AddRequire("System.Web.Abstractions, Version=0.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            template.AddRequire("System.Web.Routing, Version=0.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            template.AddRequire("System.Web.Mvc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            string prescript = @"
def $view_data.method_missing(methodname)
    $view_data.get_Item(methodname.to_s)
end
";

			string script = prescript + template.ToScript();

			try
			{
                ScriptSource source = rubyengine.CreateScriptSourceFromString(script);
                source.Execute();
			}
			catch (Exception e)
			{
				context.HttpContext.Response.Write(script + "<br />");
                context.HttpContext.Response.Write(e.ToString());
			}
		}
    }
}
