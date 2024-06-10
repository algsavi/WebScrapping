using AeC.WebScrapping.Application.Services;
using AeC.WebScrapping.Domain.Interfaces;
using AeC.WebScrapping.Infra.Context;
using AeC.WebScrapping.Infra.Repositories;
using AeC.WebScrapping.Infra.WebExtractor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AeC.WebScrapping.IoC;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IWebExtractor, WebExtractor>();
        services.AddScoped<IScrappingService, ScrappingService>();
        services.AddScoped<IExtractorRepository, ExtractorRepository>();

        var sqlConnectionString = configuration.GetConnectionString("sqlConnectionString");

        services.AddDbContext<WebScrapperDbContext>(options =>
            options.UseSqlServer(sqlConnectionString)
        );

        services.AddSingleton<IWebDriver>(provider =>
        {
            var userAgent = configuration.GetSection("UserAgent");

            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--user-agent=" + userAgent);
            return new ChromeDriver(chromeOptions);
        });

        return services;
    }
}