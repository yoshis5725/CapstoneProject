using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Server.Controllers;


[ApiController]
[Route("api/[controller]")]


public class CategoryController : Controller
{
    // *** FIELDS ***
    private readonly ICategoryService _categoryService;

    
    /// <summary>
    /// Constructor - dependency injection
    /// </summary>
    /// <param name="categoryService"></param>
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Category>>>> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategories();
        return Ok(categories);
    }
}