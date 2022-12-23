using Duende.IdentityServer.Extensions;
using ForuMe.Services.Identity.DbContexts;
using ForuMe.Services.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
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
        [Route("{id}")]
        public async Task<ResponseDto> Get(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                _response.Result = user;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        public async  Task<ResponseDto> Put([FromBody] dynamic model)
        {
            try
            {
                var data = JsonConvert.DeserializeObject(Convert.ToString(model));

                var userId = (string)data["Id"];
                var user = _userManager.Users.Where(x => x.Id == userId).FirstOrDefault();
                user.Level += (double)data["Exp"];

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
