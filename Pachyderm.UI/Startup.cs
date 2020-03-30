using Microsoft.Extensions.DependencyInjection;
using WebWindows.Blazor;

namespace Pachyderm.UI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(DesktopApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
