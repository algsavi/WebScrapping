namespace AeC.WebScrapping.Domain.Interfaces;

public interface IWebExtractor
{
    Task<List<dynamic>> ScrapeAsync(string json);
}
