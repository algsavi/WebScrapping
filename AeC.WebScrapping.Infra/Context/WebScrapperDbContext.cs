using AeC.WebScrapping.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AeC.WebScrapping.Infra.Context;

public class WebScrapperDbContext : DbContext
{
    public WebScrapperDbContext(DbContextOptions<WebScrapperDbContext> options)
        : base(options)
    { }

    public virtual DbSet<Scrapping> Scrapping { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
