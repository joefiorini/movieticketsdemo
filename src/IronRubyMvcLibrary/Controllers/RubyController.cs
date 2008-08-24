using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Mvc;
using IronRubyViewEngine;
using Ruby.Builtins;
using System.Web.Routing;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace IronRubyMvcLibrary {
    public class RubyController : Controller {

        public RubyController() {
            ViewEngine = new RubyViewEngine();
        }

        public static readonly MethodInfo InvokeActionMethod = typeof(RubyController).GetMethod("InvokeAction");

        public string ControllerName {
            get;
            set;
        }

        private Dictionary<object, object> _viewData = new Dictionary<object, object>();

        protected override void Execute(ControllerContext controllerContext) {
            ActionInvoker = new RubyControllerActionInvoker(controllerContext) { Controller = ControllerName + "Controller" };
            base.Execute(controllerContext);
        }

        [NonAction]
        public object InvokeAction(Func<object> __action) {
            return __action();
        }

        [NonAction]
        public new ViewResult View() {
            return View(null /* viewName */, null /* masterName */, null /* model */);
        }

        [NonAction]
        public new ViewResult View(object model) {
            return View(null /* viewName */, null /* masterName */, model);
        }

        [NonAction]
        public new ViewResult View(string viewName) {
            return View(viewName, null /* masterName */, null /* model */);
        }

        [NonAction]
        public new ViewResult View(string viewName, string masterName) {
            return View(viewName, masterName, null /* model */);
        }

        [NonAction]
        public new ViewResult View(string viewName, object model) {
            return View(viewName, null /* masterName */, model);
        }

        protected override ViewResult View(string viewName, string masterName, object model) {
            ViewResult newResult = new ViewResult() { ViewName = viewName, MasterName = masterName, TempData = TempData, ViewEngine = ViewEngine };
            // create new VDD
            ViewDataDictionary vdd = new ViewDataDictionary();
            foreach (var entry in _viewData) {
                vdd[Convert.ToString(entry.Key, CultureInfo.InvariantCulture)] = entry.Value;
            }
            Hash hash = model as Hash;
            vdd.Model = (hash != null) ? new HashWrapper(hash) : model;
            newResult.ViewData = vdd;
            newResult.ViewData["__scriptRuntime"] = ScriptRuntime;
            return newResult;
        }

        [NonAction]
        public new IDictionary ViewData() {
            return _viewData;
        }

        private IDictionary<object, object> _params;

        public IDictionary<object, object> Params {
            get {
                if (_params == null) {
                    var request = ControllerContext.HttpContext.Request;
                    _params = new Dictionary<object, object>(ControllerContext.RouteData.Values.Count + request.QueryString.Count + request.Form.Count);
                    foreach (var item in ControllerContext.RouteData.Values) {
                        SymbolId key = SymbolTable.StringToId(item.Key);
                        _params[key] = item.Value;
                    }
                    foreach (string key in request.QueryString.Keys) {
                        SymbolId symbolKey = SymbolTable.StringToId(key);
                        _params[symbolKey] = request.QueryString[key];
                    }
                    foreach (string key in request.Form.Keys) {
                        SymbolId symbolKey = SymbolTable.StringToId(key);
                        _params[symbolKey] = request.Form[key];
                    }
                }
                return _params;
            }
        }

        internal ScriptRuntime ScriptRuntime { get; set; }

        public ActionResult RedirectToRoute(Hash values) {
            return RedirectToRoute(values.ToRouteDictionary());
        }

        public ActionResult RedirectToAction(string actionName, Hash values) {
            return RedirectToAction(actionName, values.ToRouteDictionary());
        }

        public decimal ToDecimal(object o) {
            return Convert.ToDecimal(o);
        }
    }
}
