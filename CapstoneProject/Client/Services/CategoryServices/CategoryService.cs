using System.Net.Http.Json;

namespace CapstoneProject.Client.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    // *** FIELDS ***
    private readonly HttpClient _httpClient;


    /// <summary>
    /// Constructor - dependency injection
    /// </summary>
    /// <param name="httpClient"></param>
    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    

    public async Task<ServiceResponse<List<Category>>?> GetAllCategories()
    {
        var categories = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Category");
        return categories;
    }
}