using FluentValidation;
using UrlShortener.Service.DTOs;

namespace UrlShortener.Service.Validations;

public class CreateUrlRequestValidator : AbstractValidator<CreateUrlRequest>
{
    public CreateUrlRequestValidator()
    {
        RuleFor(request => request.TargetUrl)
            .NotEmpty()
            .WithMessage("{PropertyValue} is required and must not have an empty string")
            .Must(LinkMustBeAUri)
            .WithMessage("Link {PropertyValue} must be a valid URI");
    }

    private static bool LinkMustBeAUri(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}