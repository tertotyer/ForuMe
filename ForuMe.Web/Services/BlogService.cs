using ForuMe.Web.Models;
using ForuMe.Web.Services.IServices;

namespace ForuMe.Web.Services
{
    public class BlogService : BaseService, IBlogService
    {
        private readonly IHttpClientFactory _cliendFactory;
        public BlogService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _cliendFactory = clientFactory;
        }

        public async Task<T> CreateBlogAsync<T>(BlogDto blogDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            { 
                ApiType = SD.ApiType.POST,
                Data = blogDto,
                Url = SD.BlogAPIBase + "api/blogs/",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteBlogAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.BlogAPIBase + "api/blogs/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> GetAllBlogByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BlogAPIBase + "api/blogs/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> GetAllBlogsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BlogAPIBase + "api/blogs/",
                AccessToken = ""
            });
        }

        public async Task<T> UpdateBlogAsync<T>(BlogDto blogDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = blogDto,
                Url = SD.BlogAPIBase + "api/blogs/",
                AccessToken = ""
            });
        }
    }
}
