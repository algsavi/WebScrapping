using AeC.WebScrapping.Domain;
using AeC.WebScrapping.Domain.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AeC.WebScrapping.Application.Services;

public class ScrappingService : IScrappingService
{
    private readonly IWebExtractor _webExtractor;
    private readonly IExtractorRepository _extractorRepository;

    public ScrappingService(IWebExtractor webExtractor, IExtractorRepository extractorRepository)
    {
        _webExtractor = webExtractor;
        _extractorRepository = extractorRepository;
    }

    public async Task<Scrapping> ScrapeAsync(string json)
    {
        try
        {
            var scrappedData = await _webExtractor.ScrapeAsync(json);

            _extractorRepository.InsertData(scrappedData);

            return scrappedData;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
