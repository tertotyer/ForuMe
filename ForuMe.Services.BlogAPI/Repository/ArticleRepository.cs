using AutoMapper;
using ForuMe.Services.BlogAPI.DbContexts;
using ForuMe.Services.BlogAPI.Models;
using ForuMe.Services.BlogAPI.Models.Dtos;
using ForuMe.Services.BlogAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ForuMe.Services.BlogAPI.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ArticleRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ArticleDto> CreateUpdateArticle(ArticleDto articleDto)
        {
            var article = _mapper.Map<Article>(articleDto);
            if (article.Id > 0)
            {
                _db.Articles.Update(article);
            }
            else
            {
                _db.Articles.Add(article);
            }

            await _db.SaveChangesAsync();
            return _mapper.Map<ArticleDto>(article);
        }

        public async Task<bool> DeleteArticle(int articleId)
        {
            try
            {
                var article = await _db.Articles.FirstOrDefaultAsync(x => x.Id == articleId);
                if (article == null)
                {
                    return false;
                }
                _db.Articles.Remove(article);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ArticleDto> GetArticleById(int articleId)
        {
            var article = await _db.Articles.FirstOrDefaultAsync(x => x.Id == articleId);
            return _mapper.Map<ArticleDto>(article);
        }

        public async Task<IEnumerable<ArticleDto>> GetArticles()
        {
            var articles = await _db.Articles.ToListAsync();
            return _mapper.Map<List<ArticleDto>>(articles);
        }
    }
}
