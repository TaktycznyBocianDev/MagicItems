using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MagicItems.UI;
using MudBlazor.Services;
using MagicItems.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000/") });

Console.WriteLine("Current base adress: " + builder.HostEnvironment.BaseAddress);

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ItemsService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
