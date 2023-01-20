namespace CapstoneProject.Server.Services.CartServices;

public class CartService : ICartService
{
    // *** FIELDS ***
    private readonly MyDbContext _dbContext;

    
    /// <summary>
    /// Constructor - Dependency Injection 
    /// </summary>
    /// <param name="dbContext"></param>
    public CartService(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    // *** METHODS ***
    
    
    /// <summary>
    /// Going through each cart Item, retrieving each product, product variant, and type to create a new response
    /// </summary>
    /// <param name="cartItems">The cart items that are in the local storage</param>
    /// <returns></returns>
    public async Task<ServiceResponse<List<CartProductResponseDTO>>> GetCartProducts(List<CartItem> cartItems)
    {
        var serviceResponse = new ServiceResponse<List<CartProductResponseDTO>>
        {
            Data = new List<CartProductResponseDTO>()
        };

        foreach (var item in cartItems)
        {
            // Retrieving the product
            var product = await _dbContext.Products
                .Where(p => p.ProductId == item.ProductId)
                .FirstOrDefaultAsync();
            
            if (product == null) continue;

            // Retrieving the product variant and product type
            var productVariant = await _dbContext.ProductVariants
                .Where(pv => pv.ProductId == item.ProductId && pv.ProductTypeId == item.ProductTypeId)
                .Include(pv => pv.ProductType)
                .FirstOrDefaultAsync();
            
            if (productVariant == null) continue;

            // Creating the cartProduct
            var cartProduct = new CartProductResponseDTO
            {
                ProductId = product.ProductId,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Price = productVariant.Price,
                Quantity = item.Quantity,
                ProductTypeId = productVariant.ProductTypeId,
                ProductType = productVariant.ProductType?.Name,
            };
            
            // Adding the cartProduct to the dataList
            serviceResponse.Data.Add(cartProduct);
        }

        return serviceResponse;
    }
}