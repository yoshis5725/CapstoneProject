global using Microsoft.EntityFrameworkCore;
global using CapstoneProject.Shared.Models;
global using CapstoneProject.Shared.ServiceResponse;
global using CapstoneProject.Server.Services.CategoryServices;
using CapstoneProject.Server.Data;
using CapstoneProject.Server.Services.ProductServices;

var builder = WebApplication.CreateBuilder(args);


// *** DBCONTEXT INJECTION ***
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyDbContext>(options =>
{
    if (connectionString != null) options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


// *** DEPENDENCY INJECTIONS ***
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


// *** SWAGGER MIDDLEWARE ***
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// *** SWAGGER MIDDLEWARE ***
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

