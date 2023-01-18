using Blazored.LocalStorage;

namespace CapstoneProject.Client.Services.CartItemServices;

public class CartItemService : ICartItemService
{
    // *** FIELDS ***
    public event Action? OnCartChange;
    private readonly ILocalStorageService _localStorageService;


    // *** Constructor ***
    public CartItemService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }
    
    
    // *** METHODS ***
    
    
    /// <summary>
    /// Retrieving the cart items from the local storage. If the cart does not exist in the local storage, then creating
    /// an empty cart object.
    /// </summary>
    /// <returns></returns>
    private async Task<List<CartItem>> GetItemsInCart()
    {
        var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart") ?? new List<CartItem>();
        return cart;
    }
    
    
    /// <summary>
    /// Adding items into the cart. If there is no cart in the local storage, then creating one. Once the cart is created
    /// in the local storage, then adding the cartItem object into the cart then saving that cart object to the
    /// local storage. 
    /// </summary>
    /// <param name="cartItem"></param>
    public async Task AddToCart(CartItem cartItem)
    {
        var cart = await GetItemsInCart();
        cart.Add(cartItem);
        await _localStorageService.SetItemAsync("cart", cart);
        OnCartChange?.Invoke();
    }
    
    
    public async Task<List<CartItem>> GetAllCartItems()
    {
        var cart = await GetItemsInCart();
        return cart;
    }
}