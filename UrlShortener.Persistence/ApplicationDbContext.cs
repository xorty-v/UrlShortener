using UrlShortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Persistence.Configurations;

namespace UrlShortener.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) => Database.Migrate();

    public DbSet<Url> Urls { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UrlConfiguration());

        base.OnModelCreating(builder);
    }
}