using Microsoft.Extensions.DependencyInjection;
using Pachyderm.Services.UI;
using WebWindows.Blazor;

namespace Pachyderm
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ModalService>();
        }

        public void Configure(DesktopApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
