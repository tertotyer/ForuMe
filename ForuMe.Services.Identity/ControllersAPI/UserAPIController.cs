using Duende.IdentityServer.Extensions;
using ForuMe.Services.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForuMe.Services.Identity.ControllersAPI
{
    [Route("api/users")]
    public class UserAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserAPIController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                _response.Result = HttpContext.User.Identity.Name;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        public async  Task<ResponseDto> Put([FromBody] double experience)
        {
            try
            {
                var user = _userManager.Users.Where(x => x.Id == "770d722f-0c66-45a4-92fa-a1cdba03c737").FirstOrDefault();
                user.Level += experience;

                var result = await _userManager.UpdateAsync(user);
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
