using ForuMe.Services.BlogAPI.Models.Dtos;

namespace ForuMe.Services.BlogAPI.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<CategoryDto> GetCategoryById(int categoryId);
        Task<CategoryDto> CreateUpdateCategory(CategoryDto categoryDto);
        Task<bool> DeleteCategory(int categoryId);
    }
}
