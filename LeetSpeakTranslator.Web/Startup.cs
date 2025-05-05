using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LeetSpeakTranslator.Web.Startup))]
namespace LeetSpeakTranslator.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}
