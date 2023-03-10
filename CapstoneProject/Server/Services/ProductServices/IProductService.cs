using CapstoneProject.Shared.ServiceResponse;

namespace CapstoneProject.Server.Services.ProductServices;

public interface IProductService
{
    public List<Product> Products { get; set; }
    Task<ServiceResponse<List<Product>>> GetAllProducts();
    Task<ServiceResponse<Product>> GetSingleProduct(int productId);
    Task<ServiceResponse<List<Product>>> GetProductByCategory(string categoryUrl);
    Task<ServiceResponse<ProductSearchResultDTO>> GetSearchProducts(string searchText, int requestedPage);
    Task<ServiceResponse<List<string>>> GetProductsSearchSuggestions(string searchText);
    Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
}