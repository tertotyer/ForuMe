using ForuMe.Services.BlogAPI.Models.Dtos;
using ForuMe.Web.Models;

namespace ForuMe.Web.Services.IServices
{
    public interface IUserService : IBaseService
    {
        Task<T> UpdateUserLevelAsync<T>(dynamic data);
        Task<T> GetUserByIdAsync<T>(string id);
    }
}
