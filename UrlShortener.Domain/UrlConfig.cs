namespace UrlShortener.Domain;

public class UrlConfig
{
    public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    public const string ShortUrlRegexPattern = "^([A-Za-z0-9]+)$";
    public const int ShortUrlMinLength = 5;
}