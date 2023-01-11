using CapstoneProject.Server.Services.ProductServices;
using CapstoneProject.Shared.ServiceResponse;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Server.Controllers;


[ApiController]
[Route("api/[controller]")]


public class ProductController : Controller
{
    // *** FIELDS ***
    private readonly IProductService _productService;
    
    
    /// <summary>
    /// Constructor  - injecting productService
    /// </summary>
    /// <param name="productService"></param>
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }


    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetAllProducts()
    {
        var products = await _productService.GetAllProducts();
        return Ok(products);
    }
    
    
    [HttpGet("{productId:int}")]
    public async Task<ActionResult<ServiceResponse<Product>>> GetSingleProduct(int productId)
    {
        var product = await _productService.GetSingleProduct(productId);
        return Ok(product);
    }
}