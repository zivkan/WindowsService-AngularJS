using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Net;
using System.Web.Http;

namespace Service.Web
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var listener = (HttpListener) app.Properties["System.Net.HttpListener"];
            listener.AuthenticationSchemes = AuthenticationSchemes.IntegratedWindowsAuthentication;

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional});

            app.UseWebApi(config);

            app.UseFileServer(new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = new PhysicalFileSystem("htdocs")
            });

            // Make sure OwinHttpListener is statically referenced in this assembly's manifest, so Visual Studio will
            // copy the DLL in the service bin folder.
            typeof (Microsoft.Owin.Host.HttpListener.OwinHttpListener).ToString();
        }

        public static IDisposable Start()
        {
            var baseUrl = "http://localhost:9000";
            return WebApp.Start<OwinStartup>(baseUrl);
        }
    }
}
