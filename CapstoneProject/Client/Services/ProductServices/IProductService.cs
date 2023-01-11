namespace CapstoneProject.Client.Services.ProductServices;

public interface IProductService
{
    public List<Product> Products { get; set; }
    Task GetAllProducts();
    Task<ServiceResponse<Product>?> GetSingleProduct(int productId);
}