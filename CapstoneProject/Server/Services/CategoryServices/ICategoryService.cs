namespace CapstoneProject.Server.Services.CategoryServices;

public interface ICategoryService
{
    Task<ServiceResponse<List<Category>>> GetAllCategories();
}