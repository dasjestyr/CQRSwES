using System.Web.Http;
using Microsoft.ApplicationInsights.Extensibility;

namespace CQRSDemo.Host
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            TelemetryConfiguration.Active.DisableTelemetry = true;
        }
    }
}
