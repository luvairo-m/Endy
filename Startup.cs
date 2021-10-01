using Endy.Bot;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Endy
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            BotModel bot = new BotModel(services.BuildServiceProvider());
            services.AddSingleton(bot);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }
    }
}
