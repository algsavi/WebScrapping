namespace AeC.WebScrapping.Domain.Interfaces;

public interface IScrappingService
{
    Task<Scrapping> ScrapeAsync(string json);
}
