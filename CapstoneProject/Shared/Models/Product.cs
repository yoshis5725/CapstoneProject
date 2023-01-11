namespace CapstoneProject.Shared.Models;

public class Product
{
    public int ProductId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    
    
    // *** Category FK ***
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
}