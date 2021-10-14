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
            BotModel bot = ConfigureBot(services);
            services.AddSingleton(bot);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }

        private BotModel ConfigureBot(IServiceCollection services) =>
            new BotModel(services.BuildServiceProvider());
    }
}
