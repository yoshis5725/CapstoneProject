namespace CapstoneProject.Client.Services.CartItemServices;

public interface ICartItemService
{
    event Action OnCartChange;
    Task AddToCart(CartItem cartItem);
    Task<List<CartItem>> GetAllCartItems();
}