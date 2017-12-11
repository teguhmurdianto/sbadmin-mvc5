using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Company.Project.Web.Startup))]
namespace Company.Project.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}