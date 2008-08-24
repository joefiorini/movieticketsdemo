using System;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Hosting;
using System.IO;

namespace IronRubyViewEngine
{
	public class RubyViewEngine : IViewEngine
	{
        VirtualPathProvider vpp;

        public RubyViewEngine() : this(HostingEnvironment.VirtualPathProvider)
        { }

        public RubyViewEngine(VirtualPathProvider vpp)
        {
            this.vpp = vpp;
        }

        public void RenderView(ViewContext viewContext)
        {
            string controllerName = viewContext.RouteData.Values["controller"].ToString();
            string viewName = viewContext.ViewName;

            string pageLocation = "~/Views/" + controllerName + "/" + viewName + ".rhtml";
            VirtualFile file = vpp.GetFile(pageLocation);
            Stream stream = file.Open();
            using (StreamReader reader = new StreamReader(stream))
            {
                var page = new RubyViewPage(reader);
                page.RenderView(viewContext);
            }
        }
    }
}
