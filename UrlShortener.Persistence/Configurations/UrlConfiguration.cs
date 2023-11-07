using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Persistence.Configurations;

public class UrlConfiguration : IEntityTypeConfiguration<Url>
{
    public void Configure(EntityTypeBuilder<Url> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.ShortName).HasMaxLength(UrlConfig.ShortUrlMinLength);
        builder.HasIndex(u => u.ShortName).IsUnique();
    }
}