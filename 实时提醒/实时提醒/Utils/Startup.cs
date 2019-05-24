using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(实时提醒.Utils.Startup))]
namespace 实时提醒.Utils
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            //服务器的hub注册
        }
    }
}