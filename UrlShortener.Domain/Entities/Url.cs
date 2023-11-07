namespace UrlShortener.Domain.Entities;

public class Url
{
    public Guid Id { get; set; }
    public string TargetUrl { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public int HitCount { get; set; }
    public DateTime CreatedAt { get; set; }
}