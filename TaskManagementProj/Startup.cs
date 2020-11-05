using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskManagementProj.Startup))]
namespace TaskManagementProj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
