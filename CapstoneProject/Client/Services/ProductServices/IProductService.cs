namespace CapstoneProject.Client.Services.ProductServices;

public interface IProductService
{
    event Action ChangeOnComponent;
    public List<Product>? Products { get; set; }
    Task GetAllProducts(string? categoryUrl = null);
    Task<ServiceResponse<Product>?> GetSingleProduct(int productId);
}