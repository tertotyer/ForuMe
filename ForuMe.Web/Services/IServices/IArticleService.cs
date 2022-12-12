using ForuMe.Services.BlogAPI.Models.Dtos;

namespace ForuMe.Web.Services.IServices
{
    public interface IArticleService : IBaseService
    {
        Task<T> GetAllArticlesAsync<T>(string token);
        Task<T> GetArticleByIdAsync<T>(int id, string token);
        Task<T> CreateArticleAsync<T>(ArticleDto articleDto, string token);
        Task<T> UpdateArticleAsync<T>(ArticleDto articleDto, string token);
        Task<T> DeleteArticleAsync<T>(int id, string token);
    }
}
