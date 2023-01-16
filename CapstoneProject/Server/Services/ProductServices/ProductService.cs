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
        Products = await _myDbContext.Products
            .Include(p => p.ProductVariants)
            .ThenInclude(p => p.ProductType)
            .ToListAsync();
        serviceResponse.Data = Products;
        return serviceResponse;
    }

    
    public async Task<ServiceResponse<Product>> GetSingleProduct(int productId)
    { 
        var serviceResponse = new ServiceResponse<Product>();
        var product = await _myDbContext.Products
            .Include(p => p.ProductVariants)
            .ThenInclude(p => p.ProductType)
            .FirstOrDefaultAsync(p => p.ProductId.Equals(productId));

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

    public async Task<ServiceResponse<List<Product>>> GetProductByCategory(string categoryUrl)
    {
        var serviceResponse = new ServiceResponse<List<Product>>();
        var products = await _myDbContext.Products
            .Where(p => p.Category != null && p.Category.Url.ToLower().Equals(categoryUrl))
            .Include(p => p.ProductVariants)
            .ThenInclude(p => p.ProductType)
            .ToListAsync();
        
        serviceResponse.Data = products;
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<Product>>> GetSearchProducts(string searchText)
    {
        var serviceResponse = new ServiceResponse<List<Product>>
        {
            Data = await RequestedSearchProducts(searchText)
        };

        return serviceResponse;
    }

    
    public async Task<ServiceResponse<List<string>>> GetProductsSearchSuggestions(string searchText)
    {
        var products = await RequestedSearchProducts(searchText);
        var resultsList = new List<string>();

        foreach (var product in products)
        {
            if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            {
                resultsList.Add(product.Title);
            }

            if (product?.Description != null)
            {
                // *** Getting the punctuation, then removing them
                var punctuation = product.Description.Where(char.IsPunctuation)
                    .Distinct().ToArray();
                var words = product.Description.Split()
                    .Select(s => s.Trim(punctuation));

                // *** Checking if any of the words contains the searchText argument and is not already in the
                // resultsList. If it meets the criteria, adding the word to the list
                foreach (var word in words)
                {
                    if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase) && !resultsList.Contains(word))
                    {
                        resultsList.Add(word);
                    }
                }
            }
        }

        return new ServiceResponse<List<string>> { Data = resultsList };
    }

    
    public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
    {
        var serviceResponse = new ServiceResponse<List<Product>>
        {
            Data = await _myDbContext.Products.Where(p => p.Featured)
                .Include(p => p.ProductVariants)
                .ToListAsync()
        };

        return serviceResponse;
    }


    private async Task<List<Product>> RequestedSearchProducts(string searchText)
    {
        return await _myDbContext.Products
            .Where(p => p.Title.ToLower()
                .Contains(searchText) || p.Description.ToLower().Contains(searchText))
            .Include(p => p.ProductVariants)
            .ToListAsync();
    }
}