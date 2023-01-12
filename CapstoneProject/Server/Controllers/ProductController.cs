using CapstoneProject.Server.Services.ProductServices;
using CapstoneProject.Shared.ServiceResponse;
using Microsoft.AspNetCore.Mvc;
using ProductService = CapstoneProject.Client.Services.ProductServices.ProductService;

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


    [HttpGet("category/{categoryUrl}")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductByCategory(string categoryUrl)
    {
        var products = await _productService.GetProductByCategory(categoryUrl);
        return Ok(products);
    }
}