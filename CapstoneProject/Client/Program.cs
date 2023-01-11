﻿global using CapstoneProject.Shared.Models;
global using CapstoneProject.Client.Services.ProductServices;
global using CapstoneProject.Shared.ServiceResponse;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CapstoneProject.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// *** DEPENDENCY INJECTION ***
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

