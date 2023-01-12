using System.Net.Http.Json;

namespace CapstoneProject.Client.Services.ProductServices;

public class ProductService : IProductService
{
    // *** FIELDS ***
    private readonly HttpClient _httpClient;
    public event Action? ChangeOnComponent;
    public List<Product>? Products { get; set; }
    
    
    // *** Constructor - httpclient injection ***
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
    /// <summary>
    /// Getting all components if no categoryUrl is pass in as an argument. If an argument is received, then will filter
    /// the products based on the argument. Will then invoke an onchange action to re-render all registered components.
    /// </summary>
    /// <param name="categoryUrl">filtering based off of the categoryUrl argument</param>
    public async Task GetAllProducts(string? categoryUrl = null)
    {
        if (categoryUrl == null)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product");
            if (response != null)
                Products = response.Data;
        }
        else
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Product/category/{categoryUrl}");
            if (response != null)
                Products = response.Data;
        }
        
        ChangeOnComponent?.Invoke();
    }
    

    public async Task<ServiceResponse<Product>?> GetSingleProduct(int productId)
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");
        return response;
    }
}