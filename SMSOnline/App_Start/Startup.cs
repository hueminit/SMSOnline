using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMSOnline.Startup))]

namespace SMSOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ContainerResolve.ConfigAutofac(app);
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}