using ForuMe.Web.Models;

namespace ForuMe.Web.Services.IServices
{
    public interface ICategoryService : IBaseService
    {
        Task<T> GetAllCategoriesAsync<T>(string token);
        Task<T> GetCategoryByIdAsync<T>(int id, string token);
        Task<T> CreateCategoryAsync<T>(CategoryDto categoryDto, string token);
        Task<T> UpdateCategoryAsync<T>(CategoryDto categoryDto, string token);
        Task<T> DeleteCategoryAsync<T>(int id, string token);
    }
}
