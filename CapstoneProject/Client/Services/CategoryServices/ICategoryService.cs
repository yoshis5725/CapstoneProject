namespace CapstoneProject.Client.Services.CategoryServices;

public interface ICategoryService
{
    Task<ServiceResponse<List<Category>>?> GetAllCategories();
}