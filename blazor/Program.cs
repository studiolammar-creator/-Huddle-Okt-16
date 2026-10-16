using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Mokka.Components;
using Mokka.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<Routes>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// One AppState per browser tab: login, order and toast.
builder.Services.AddScoped<AppState>();

await builder.Build().RunAsync();
