using AeC.WebScrapping.Domain;
using AeC.WebScrapping.Domain.Interfaces;

namespace AeC.WebScrapping.Application.Services;

public class ScrappingService : IScrappingService
{
    private readonly IWebExtractor _webExtractor;
    private readonly IExtractorRepository _extractorRepository;

    public ScrappingService(IWebExtractor webExtractor)
    {
        _webExtractor = webExtractor;
    }

    public Task<List<dynamic>> ScrapeAsync(string json)
    {
        return _webExtractor.ScrapeAsync(json);
    }
}
