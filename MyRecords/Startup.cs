using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyRecords.Startup))]
namespace MyRecords
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
