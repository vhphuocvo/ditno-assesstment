using DitnoCalculateBusinessDay.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DitnoCalculateBusinessDay
{
    public class Startup
    {
        public IServiceProvider Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole();
            });
            services.AddSingleton<IBusinessDayService, BusinessDayService>();

            IServiceProvider provider = services.BuildServiceProvider();
            return provider;
        }
    }
}
