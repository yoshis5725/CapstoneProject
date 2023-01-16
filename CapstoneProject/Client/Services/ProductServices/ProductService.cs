using System.Net.Http.Json;

namespace CapstoneProject.Client.Services.ProductServices;

public class ProductService : IProductService
{
    // *** FIELDS ***
    private readonly HttpClient _httpClient;
    public event Action? ChangeOnComponent;
    public string Message { get; set; }
    public List<Product>? Products { get; set; }
    
    
    // *** Pagination Fields ***
    public int CurrentPage { get; set; } = 1;
    public int PageCount { get; set; } = 0;
    public string LastSearchText { get; set; } = string.Empty;
    
    
    
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
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product/featured");
            if (response != null)
                Products = response.Data;
        }
        else
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Product/category/{categoryUrl}");
            if (response != null)
                Products = response.Data;
        }
        
        // *** PAGINATION ***
        CurrentPage = 1;
        PageCount = 0;

        if (PageCount == 0)
            Message = "No products found!";
        
        ChangeOnComponent?.Invoke();
    }
    

    public async Task<ServiceResponse<Product>?> GetSingleProduct(int productId)
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");
        return response;
    }

    
    public async Task SearchProducts(string searchText, int page)
    {
        LastSearchText = searchText;
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductSearchResultDTO>>($"api/Product/search/{searchText}/{page}");

        if (response is { Data: { } })
        {
            Products = response?.Data.Products;
            CurrentPage = response.Data.CurrentPage;
            PageCount = response.Data.Pages;
        }
        
        if (Products?.Count < 1)
            Message = $"No products found!";
        
        ChangeOnComponent?.Invoke();
    }

    
    public async Task<List<string>?> SearchProductSuggestions(string searchText)
    {
        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/Product/search/suggestions/{searchText}");
        return response?.Data;
    }
}