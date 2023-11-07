using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;
using UrlShortener.Persistence.Interfaces;

namespace UrlShortener.Persistence.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UrlRepository(ApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task AddAsync(Url url)
    {
        await _dbContext.Urls.AddAsync(url);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Url url)
    {
        _dbContext.Urls.Update(url);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Url> GetTargetUrlAsync(string shortName)
    {
        return await _dbContext.Urls.FirstOrDefaultAsync(u => u.ShortName == shortName);
    }

    public async Task<bool> IsShortNameExistAsync(string shortName)
    {
        return await _dbContext.Urls.AnyAsync(u => u.ShortName == shortName);
    }

    public async Task<string> GetShortNameAsync(string targetUrl)
    {
        return (await _dbContext.Urls.FirstOrDefaultAsync(u => u.TargetUrl == targetUrl))?.ShortName;
    }
}