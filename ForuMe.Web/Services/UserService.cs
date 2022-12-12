using ForuMe.Web.Models;
using ForuMe.Web.Services.IServices;

namespace ForuMe.Web.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IHttpClientFactory _cliendFactory;
        public UserService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _cliendFactory = clientFactory;
        }

        public async Task<T> UpdateUserLevel<T>(double experience)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = experience,
                Url = SD.IdentityAPIBase + "api/users/"
            });
        }
    }
}
