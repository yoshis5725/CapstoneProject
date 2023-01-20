using System.Net.Http.Json;
using Blazored.LocalStorage;

namespace CapstoneProject.Client.Services.CartItemServices;

public class CartItemService : ICartItemService
{
    // *** FIELDS ***
    public event Action? OnCartChange;
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;


    // *** Constructor ***
    public CartItemService(ILocalStorageService localStorageService, HttpClient httpClient)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClient;
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
        
        // querying the cart to find if the item already exists in the cart
        var sameItem = cart.Find(
            c => c.ProductId == cartItem.ProductId 
                 && c.ProductTypeId == cartItem.ProductTypeId
                 );

        if (sameItem == null)
        {
            cart.Add(cartItem);
        }
        else
        {
            sameItem.Quantity += cartItem.Quantity;
        }
        
        await _localStorageService.SetItemAsync("cart", cart);
        OnCartChange?.Invoke();
    }
    
    
    public async Task<List<CartItem>> GetAllCartItems()
    {
        var cart = await GetItemsInCart();
        return cart;
    }

    
    public async Task<List<CartProductResponseDTO>> GetCartProducts()
    {
        var cartItems = await _localStorageService.GetItemAsync<List<CartItem>>("cart");
        var response = await _httpClient.PostAsJsonAsync("api/Cart/products", cartItems);
        var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponseDTO>>>();
        return cartProducts?.Data;
    }

    public async Task RemoveProductFromCart(int productId, int productTypeId)
    {
        var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart");

        var cartItem = cart?.Find(
            c => c.ProductId == productId
                 && c.ProductTypeId == productTypeId
        );

        if (cartItem != null)
        {
            cart?.Remove(cartItem);
            await _localStorageService.SetItemAsync("cart", cart);
            OnCartChange?.Invoke();
        }
    }

    public async Task UpdateQuantity(CartProductResponseDTO product)
    {
        var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart");

        var cartItem = cart?.Find(
            c => c.ProductId == product.ProductId
                 && c.ProductTypeId == product.ProductTypeId
        );

        if (cartItem != null)
        {
            cartItem.Quantity = product.Quantity;
            await _localStorageService.SetItemAsync("cart", cart);
        }
    }
}