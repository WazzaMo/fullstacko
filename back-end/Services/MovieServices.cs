
using Models;
using Services;
using Microsoft.AspNetCore.Mvc;

namespace Services;

public class MovieServices
{
    public static IHostBuilder ConfigureServices(IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddSingleton<IIntegrationProvider, MoviesIntegrationProvider>();
        });

        return hostBuilder;
    }
}