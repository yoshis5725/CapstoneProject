using CapstoneProject.Server.Data;

namespace CapstoneProject.Server.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    // *** FIELDS ***
    private readonly MyDbContext _myDbContext;


    /// <summary>
    /// Constructor - injecting the context
    /// </summary>
    /// <param name="myDbContext"></param>
    public CategoryService(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }
    
    
    
    public async Task<ServiceResponse<List<Category>>> GetAllCategories()
    {
        var serviceResponse = new ServiceResponse<List<Category>>();
        var categories = await _myDbContext.Categories.ToListAsync();

        if (categories != null)
            serviceResponse.Data = categories;

        return serviceResponse;
    }
}