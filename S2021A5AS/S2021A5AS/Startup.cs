using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(S2021A5AS.Startup))]

namespace S2021A5AS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
