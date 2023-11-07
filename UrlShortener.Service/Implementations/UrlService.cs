using UrlShortener.Domain;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Exceptions;
using UrlShortener.Persistence.Interfaces;
using UrlShortener.Service.DTOs;
using UrlShortener.Service.Interfaces;

namespace UrlShortener.Service.Implementations;

public class UrlService : IUrlService
{
    private readonly IUrlRepository _urlRepository;

    public UrlService(IUrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }

    public async Task<UrlResponse> AddAsync(string targetUrl)
    {
        var shortName = await _urlRepository.GetShortNameAsync(targetUrl);

        if (shortName != null)
            return new UrlResponse() { Link = shortName };

        shortName = await GenerateUniqueCode();

        var url = new Url()
        {
            TargetUrl = targetUrl,
            ShortName = shortName,
            CreatedAt = DateTime.UtcNow
        };

        await _urlRepository.AddAsync(url);

        return new UrlResponse() { Link = shortName };
    }

    public async Task<UrlResponse> GetTargetUrlAsync(string shortName)
    {
        var targetUrl = await _urlRepository.GetTargetUrlAsync(shortName);

        if (targetUrl == null)
            throw new UrlNotFoundException("The requested page was not found");

        targetUrl.HitCount++;
        await _urlRepository.UpdateAsync(targetUrl);

        return new UrlResponse() { Link = targetUrl.TargetUrl };
    }


    private async Task<string> GenerateUniqueCode()
    {
        var random = new Random();
        var codeChars = new char[UrlConfig.ShortUrlMinLength];

        while (true)
        {
            for (int i = 0; i < UrlConfig.ShortUrlMinLength; i++)
            {
                var randomIndex = random.Next(UrlConfig.Alphabet.Length - 1);

                codeChars[i] = UrlConfig.Alphabet[randomIndex];
            }

            var code = new string(codeChars);

            if (!await _urlRepository.IsShortNameExistAsync(code))
            {
                return code;
            }
        }
    }
}