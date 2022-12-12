using ForuMe.Web.Models;
using ForuMe.Web.Services.IServices;

namespace ForuMe.Web.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IHttpClientFactory _cliendFactory;
        public CategoryService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _cliendFactory = clientFactory;
        }

        public async Task<T> CreateCategoryAsync<T>(CategoryDto categoryDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = categoryDto,
                Url = SD.BlogAPIBase + "api/categories/",
                AccessToken = token
            });
        }

        public async Task<T> DeleteCategoryAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.BlogAPIBase + "api/categories/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllCategoriesAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BlogAPIBase + "api/categories/",
                AccessToken = token
            });
        }

        public async Task<T> GetCategoryByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BlogAPIBase + "api/categories/" + id,
                AccessToken = token
            });
        }

        public async Task<T> UpdateCategoryAsync<T>(CategoryDto categoryDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = categoryDto,
                Url = SD.BlogAPIBase + "api/categories/",
                AccessToken = token
            });
        }
    }
}
