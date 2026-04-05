using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BookStack.Client.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Catalog API – im Docker-Betrieb über localhost:8080 erreichbar
var catalogBaseUrl = builder.Configuration["CatalogApi:BaseUrl"] ?? "http://localhost:8080";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(catalogBaseUrl) });

await builder.Build().RunAsync();
