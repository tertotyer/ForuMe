using ForuMe.Services.BlogAPI.Models.Dtos;
using ForuMe.Services.BlogAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForuMe.Services.BlogAPI.Controllers
{
    [Route("api/blogs")]
    public class BlogAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IBlogRepository _blogRepository;

        public BlogAPIController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                var blogDtos = await _blogRepository.GetBlogs();
                _response.Result = blogDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                var blogDto = await _blogRepository.GetBlogById(id);
                _response.Result = blogDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] BlogDto blogDto)
        {   
            try
            {
                var model = await _blogRepository.CreateUpdateBlog(blogDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] BlogDto blogDto)
        {
            try
            {
                var model = await _blogRepository.CreateUpdateBlog(blogDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {   
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                var isSuccess = await _blogRepository.DeleteBlog(id);
                _response.Result = isSuccess;
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
