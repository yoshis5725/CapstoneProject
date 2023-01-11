using CapstoneProject.Server.Data;
using CapstoneProject.Shared.ServiceResponse;

namespace CapstoneProject.Server.Services.ProductServices;

public class ProductService : IProductService
{
    // *** FIELDS ***
    private readonly MyDbContext _myDbContext;
    public List<Product> Products { get; set; }


    /// <summary>
    /// Constructor - injecting database context
    /// </summary>
    /// <param name="myDbContext"></param>
    public ProductService(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }
    
    
    
    public async Task<ServiceResponse<List<Product>>> GetAllProducts()
    {
        var serviceResponse = new ServiceResponse<List<Product>>();
        Products = await _myDbContext.Products.ToListAsync();
        serviceResponse.Data = Products;
        return serviceResponse;
    }

    
    public async Task<ServiceResponse<Product>> GetSingleProduct(int productId)
    { 
        var serviceResponse = new ServiceResponse<Product>();
        var product = await _myDbContext.Products.FindAsync(productId);
        
        if (product == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "This product does not exist in the database";
        }
        else
        {
            serviceResponse.Data = product;
        }
        return serviceResponse;
    }
}