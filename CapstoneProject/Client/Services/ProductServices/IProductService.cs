namespace CapstoneProject.Client.Services.ProductServices;

public interface IProductService
{
    event Action ChangeOnComponent;
    public string Message { get; set; }  // this will be used for the search result messaging 
    public List<Product>? Products { get; set; }
    Task GetAllProducts(string? categoryUrl = null);
    Task<ServiceResponse<Product>?> GetSingleProduct(int productId);
    Task SearchProducts(string searchText);
    Task<List<string>?> SearchProductSuggestions(string searchText);
}