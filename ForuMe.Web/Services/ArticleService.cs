using ForuMe.Services.BlogAPI.Models.Dtos;
using ForuMe.Web.Models;
using ForuMe.Web.Services.IServices;

namespace ForuMe.Web.Services
{
    public class ArticleService : BaseService, IArticleService
    {
        private readonly IHttpClientFactory _cliendFactory;

        public ArticleService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _cliendFactory = clientFactory;
        }

        public async Task<T> CreateArticleAsync<T>(ArticleDto articleDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = articleDto,
                Url = SD.BlogAPIBase + "api/articles/",
                AccessToken = token
            });
        }

        public async Task<T> DeleteArticleAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.BlogAPIBase + "api/articles/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllArticlesAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BlogAPIBase + "api/articles/",
                AccessToken = token
            });
        }

        public async Task<T> GetArticleByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BlogAPIBase + "api/articles/" + id,
                AccessToken = token
            });
        }

        public async Task<T> UpdateArticleAsync<T>(ArticleDto articleDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = articleDto,
                Url = SD.BlogAPIBase + "api/articles/",
                AccessToken = token
            });
        }
    }
}
