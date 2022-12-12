using ForuMe.Services.BlogAPI.Models.Dtos;

namespace ForuMe.Services.BlogAPI.Repository.IRepository
{
    public interface IArticleRepository
    {
        Task<IEnumerable<ArticleDto>> GetArticles();
        Task<ArticleDto> GetArticleById(int articleId);
        Task<ArticleDto> CreateUpdateArticle(ArticleDto articleDto);
        Task<bool> DeleteArticle(int articleId);
    }
}
