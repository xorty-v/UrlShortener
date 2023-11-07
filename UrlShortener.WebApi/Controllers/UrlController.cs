using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Service.DTOs;
using UrlShortener.Service.Interfaces;

namespace UrlShortener.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlController : ControllerBase
{
    private readonly IUrlService _urlService;

    public UrlController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUrlRequest urlRequest,
        [FromServices] IValidator<CreateUrlRequest> validator)
    {
        ValidationResult validationResult = await validator.ValidateAsync(urlRequest);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var response = await _urlService.AddAsync(urlRequest.TargetUrl);

        return Ok(response.Link);
    }

    [HttpGet("/{shortName}")]
    public async Task<IActionResult> Get(string shortName)
    {
        var response = await _urlService.GetTargetUrlAsync(shortName);

        return Ok(response.Link);
    }
}