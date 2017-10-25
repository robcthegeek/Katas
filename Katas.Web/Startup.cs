using Owin;

namespace Katas.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<HelloWorldMiddleware>();
        }
    }
}