using DragonchainSDK;
using FitzyBot.Application;
using FitzyBot.Application.Services;
using FitzyBot.Core;
using FitzyBot.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FitzyBot.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {   
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>(true)
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .Build();

            var services = ConfigureServices(configuration);

            var serviceProvider = services.BuildServiceProvider();

            IServiceScope scope = serviceProvider.CreateScope();

            scope.ServiceProvider.GetRequiredService<FitzyBot>().Run();

            Console.ReadLine();
        }

        private static IServiceCollection ConfigureServices(IConfiguration configuration)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton(configuration);

            services.Configure<TwitchConfigurationOptions>(configuration.GetSection("twitchClient"));
            
            services.AddSingleton<IDragonchainClient, DragonchainClient>();

            //services.AddTransient<ILoyaltyService, InMemoryLoyaltyService>();

            services.AddTransient<ILoyaltyService, SimpleDragonLoyaltyService>();

            services.AddTransient<FitzyBot>();

            return services;
        }

        private static void DisposeServices(ServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                return;
            }
            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}
