namespace CapstoneProject.Server.Services.CartServices;

public interface ICartService
{
    Task<ServiceResponse<List<CartProductResponseDTO>>> GetCartProducts(List<CartItem> cartItems);
}