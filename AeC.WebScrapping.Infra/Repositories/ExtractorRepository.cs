using AeC.WebScrapping.Domain;
using AeC.WebScrapping.Domain.Interfaces;
using AeC.WebScrapping.Infra.Context;

namespace AeC.WebScrapping.Infra.Repositories;

public class ExtractorRepository : IExtractorRepository
{
    private readonly WebScrapperDbContext _context;

    public ExtractorRepository(WebScrapperDbContext context)
    {
        _context = context;
    }
    public void InsertData(Scrapping webData)
    {
        _context.Scrapping.Add(webData);
        _context.SaveChanges();
    }
}
