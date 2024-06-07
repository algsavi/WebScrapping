namespace AeC.WebScrapping.Domain.Interfaces;

public interface IWebExtractor
{
    Task<Scrapping> ScrapeAsync(string json);
}
