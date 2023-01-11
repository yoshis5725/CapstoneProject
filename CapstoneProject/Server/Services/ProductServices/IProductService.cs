using CapstoneProject.Shared.ServiceResponse;

namespace CapstoneProject.Server.Services.ProductServices;

public interface IProductService
{
    public List<Product> Products { get; set; }
    Task<ServiceResponse<List<Product>>> GetAllProducts();
    Task<ServiceResponse<Product>> GetSingleProduct(int productId);
}