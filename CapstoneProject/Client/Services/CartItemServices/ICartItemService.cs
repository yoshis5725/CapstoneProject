namespace CapstoneProject.Client.Services.CartItemServices;

public interface ICartItemService
{
    event Action OnCartChange;
    Task AddToCart(CartItem cartItem);
    Task<List<CartItem>> GetAllCartItems();
    Task<List<CartProductResponseDTO>> GetCartProducts();
    Task RemoveProductFromCart(int productId, int productTypeId);
    Task UpdateQuantity(CartProductResponseDTO product);
}