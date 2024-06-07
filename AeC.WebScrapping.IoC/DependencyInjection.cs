using AeC.WebScrapping.Application.Services;
using AeC.WebScrapping.Domain.Interfaces;
using AeC.WebScrapping.Infra.WebExtractor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace AeC.WebScrapping.IoC;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IWebExtractor, WebExtractor>();
        services.AddScoped<IScrappingService, ScrappingService>();

        services.AddSingleton<IWebDriver>(provider =>
        {
            var userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.82 Safari/537.36";

            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--user-agent=" + userAgent);
            return new ChromeDriver(chromeOptions);
        });

        return services;
    }
}