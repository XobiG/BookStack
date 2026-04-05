# Build-Image (Context = Solution-Root)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# NuGet-Restore mit Caching über separates csproj-COPY
COPY BookStack.Shared/BookStack.Shared.csproj                   BookStack.Shared/
COPY BookStack.Server.Catalog/BookStack.Server.Catalog.csproj   BookStack.Server.Catalog/
RUN dotnet restore BookStack.Server.Catalog/BookStack.Server.Catalog.csproj

# Quellcode kopieren und publishen
COPY BookStack.Shared/       BookStack.Shared/
COPY BookStack.Server.Catalog/ BookStack.Server.Catalog/
RUN dotnet publish BookStack.Server.Catalog/BookStack.Server.Catalog.csproj \
    -c Release -o /app

# Runtime-Image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "BookStack.Server.Catalog.dll"]
