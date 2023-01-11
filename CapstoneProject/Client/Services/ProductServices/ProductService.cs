using System.Net.Http.Json;

namespace CapstoneProject.Client.Services.ProductServices;

public class ProductService : IProductService
{
    // *** FIELDS ***
    private readonly HttpClient _httpClient;
    public List<Product> Products { get; set; }
    
    
    // *** Constructor - httpclient injection ***
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
    public async Task GetAllProducts()
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product");
        if (response?.Data != null) 
            Products = response.Data;
    }

    
    public async Task<ServiceResponse<Product>?> GetSingleProduct(int productId)
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");
        return response;
    }
}