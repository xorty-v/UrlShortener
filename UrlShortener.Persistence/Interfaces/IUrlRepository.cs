using UrlShortener.Domain.Entities;

namespace UrlShortener.Persistence.Interfaces;

public interface IUrlRepository
{
    Task AddAsync(Url url);
    Task UpdateAsync(Url url);
    Task<Url> GetTargetUrlAsync(string shortName);
    Task<bool> IsShortNameExistAsync(string shortName);
    Task<string> GetShortNameAsync(string longUrl);
}