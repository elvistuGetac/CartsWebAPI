using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Carts.Helpers;

namespace Carts
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
			/*var Xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
			Xml.UseXmlSerializer= true;
			var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
			json.UseDataContractJsonSerializer = true;*/
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			HttpCookie cultureCookie = Request.Cookies["_culture"];
			if (cultureCookie != null)
			{
				// get culture name
				var cultureInfoName = CultureHelper.GetImplementedCulture(cultureCookie.Value);

				// set culture
				System.Threading.Thread.CurrentThread.CurrentCulture =
				new System.Globalization.CultureInfo(cultureInfoName);
				System.Threading.Thread.CurrentThread.CurrentUICulture =
				new System.Globalization.CultureInfo(cultureInfoName);

			}
		}
	}
}
