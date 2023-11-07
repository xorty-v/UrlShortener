namespace UrlShortener.Domain.Exceptions;

public sealed class UrlNotFoundException : Exception
{
    public UrlNotFoundException(string message)
        : base(message)
    {
    }
}