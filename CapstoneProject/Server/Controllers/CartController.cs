using CapstoneProject.Server.Services.CartServices;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Server.Controllers;


[ApiController]
[Route("api/[controller]")]


public class CartController : Controller
{
    // *** FIELDS ***
    private readonly ICartService _cartService;

    
    /// <summary>
    /// Constructor - Dependency Injection
    /// </summary>
    /// <param name="cartService"></param>
    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }


    [HttpPost("products")]
    public async Task<ActionResult<ServiceResponse<List<CartProductResponseDTO>>>> GetCartProducts(
        List<CartItem> cartItems)
    {
        var result = await _cartService.GetCartProducts(cartItems);
        return Ok(result);
    }
}