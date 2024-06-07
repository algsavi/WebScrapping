using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeC.WebScrapping.Domain;

namespace AeC.WebScrapping.Infra.Configuration
{
    public class ScrappingConfiguration : IEntityTypeConfiguration<Scrapping>
    {
        public void Configure(EntityTypeBuilder<Scrapping> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Url)
                .IsUnicode(false)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.Properties)
                .IsUnicode(false)
                .HasMaxLength(50)
                .HasColumnType("nvarchar(max)")
                .IsRequired();
        }
    }
}
