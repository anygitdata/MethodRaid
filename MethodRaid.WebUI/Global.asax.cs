using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using MethodRaid.WebUI.App_Start;

namespace MethodRaid.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
