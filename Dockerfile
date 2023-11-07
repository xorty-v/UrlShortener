FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UrlShortener.WebApi/UrlShortener.WebApi.csproj", "UrlShortener.WebApi/"]
COPY ["UrlShortener.Service/UrlShortener.Service.csproj", "UrlShortener.Service/"]
COPY ["UrlShortener.Persistence/UrlShortener.Persistence.csproj", "UrlShortener.Persistence/"]
COPY ["UrlShortener.Domain/UrlShortener.Domain.csproj", "UrlShortener.Domain/"]
RUN dotnet restore "UrlShortener.WebApi/UrlShortener.WebApi.csproj"
COPY . .
WORKDIR "/src/UrlShortener.WebApi"
RUN dotnet build "UrlShortener.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlShortener.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlShortener.WebApi.dll"]