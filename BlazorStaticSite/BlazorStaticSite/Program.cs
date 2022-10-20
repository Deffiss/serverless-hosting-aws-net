using BlazorStaticSite;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseUri = new Uri(builder.HostEnvironment.BaseAddress);
var apiUrl = new Uri("https://domain.com");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = apiUrl });

await builder.Build().RunAsync();
