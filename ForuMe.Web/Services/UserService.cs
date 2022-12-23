using ForuMe.Web.Models;
using ForuMe.Web.Services.IServices;
using NuGet.Common;

namespace ForuMe.Web.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IHttpClientFactory _cliendFactory;
        public UserService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _cliendFactory = clientFactory;
        }

        public async Task<T> GetUserByIdAsync<T>(string id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.IdentityAPIBase + "api/users/" + id,
            });
        }

        public async Task<T> UpdateUserLevelAsync<T>(dynamic data)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = data,
                Url = SD.IdentityAPIBase + "api/users/"
            });
        }
    }
}
