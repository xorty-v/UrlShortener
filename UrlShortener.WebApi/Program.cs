using FluentValidation;
using UrlShortener.Persistence;
using UrlShortener.WebApi.Helpers;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Service.Interfaces;
using UrlShortener.Persistence.Interfaces;
using UrlShortener.Service.Implementations;
using UrlShortener.Persistence.Repositories;
using UrlShortener.Service.DTOs;
using UrlShortener.Service.Validations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddScoped<IValidator<CreateUrlRequest>, CreateUrlRequestValidator>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration["ConnectionStrings:Database"]);
});

builder.Services.AddScoped<IUrlRepository, UrlRepository>();
builder.Services.AddScoped<IUrlService, UrlService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();