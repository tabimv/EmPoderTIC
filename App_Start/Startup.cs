using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(EmPoderTIC.App_Start.Startup))]

namespace EmPoderTIC.App_Start
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Agrega la configuración de MVC u otros servicios que necesites.

            // Agrega la configuración para IMemoryCache.
            services.AddMemoryCache();

            // Resto de la configuración...
        }

        public void Configuration(IAppBuilder app)
        {
            // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                ExpireTimeSpan = TimeSpan.FromMinutes(30),
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}
