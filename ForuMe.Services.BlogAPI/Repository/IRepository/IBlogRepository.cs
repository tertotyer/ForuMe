using ForuMe.Services.BlogAPI.Models.Dtos;

namespace ForuMe.Services.BlogAPI.Repository.IRepository
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogDto>> GetBlogs();
        Task<BlogDto> GetBlogById(int blogId);
        Task<BlogDto> CreateUpdateBlog(BlogDto blogDto);
        Task<bool> DeleteBlog(int blogId);
    }
}

