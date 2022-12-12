using ForuMe.Web.Models;

namespace ForuMe.Web.Services.IServices
{
    public interface IBlogService : IBaseService
    {
        Task<T> GetAllBlogsAsync<T>(string token);
        Task<T> GetBlogByIdAsync<T>(int id, string token);
        Task<T> CreateBlogAsync<T>(BlogDto blogDto, string token);
        Task<T> UpdateBlogAsync<T>(BlogDto blogDto, string token);
        Task<T> DeleteBlogAsync<T>(int id, string token);

    }
}
