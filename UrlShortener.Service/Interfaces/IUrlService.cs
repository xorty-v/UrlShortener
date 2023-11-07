using UrlShortener.Service.DTOs;

namespace UrlShortener.Service.Interfaces;

public interface IUrlService
{
    Task<UrlResponse> AddAsync(string targetUrl);
    Task<UrlResponse> GetTargetUrlAsync(string shortName);
}