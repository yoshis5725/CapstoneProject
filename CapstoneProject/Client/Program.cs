global using CapstoneProject.Shared.Models;
global using CapstoneProject.Client.Services.ProductServices;
global using CapstoneProject.Shared.ServiceResponse;
global using CapstoneProject.Client.Services.CartItemServices;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CapstoneProject.Client;
using CapstoneProject.Client.Services.CategoryServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// *** DEPENDENCY INJECTION ***
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();


// *** LOCAL STORAGE MIDDLEWARE ***
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

