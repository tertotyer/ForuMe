using ForuMe.Web.Models;

namespace ForuMe.Web.Services.IServices
{
    public interface IBlogService : IBaseService
    {
        Task<T> GetAllBlogsAsync<T>();
        Task<T> GetAllBlogByIdAsync<T>(int id);
        Task<T> CreateBlogAsync<T>(BlogDto blogDto);
        Task<T> UpdateBlogAsync<T>(BlogDto blogDto);
        Task<T> DeleteBlogAsync<T>(int id);

    }
}
