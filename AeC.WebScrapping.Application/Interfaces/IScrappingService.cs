namespace AeC.WebScrapping.Domain.Interfaces;

public interface IScrappingService
{
    Task<List<dynamic>> ScrapeAsync(string json);
}
