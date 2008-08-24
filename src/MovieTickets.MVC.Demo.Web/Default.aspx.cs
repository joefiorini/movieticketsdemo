using System;
using System.Collections.Generic;
using System.Web.UI;

namespace MovieTickets.MVC.Demo.Web
{
    public partial class _Default : Page
    {
        public void Page_Load(object sender, System.EventArgs e)
        {
            Response.Redirect("~/Home");
        }
    }
}
