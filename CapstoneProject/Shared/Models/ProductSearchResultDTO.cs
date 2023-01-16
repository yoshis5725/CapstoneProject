namespace CapstoneProject.Shared.Models;

public class ProductSearchResultDTO
{
    public List<Product> Products { get; set; } = new();
    public int Pages { get; set; }
    public int CurrentPage { get; set; }
}