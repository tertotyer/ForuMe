using ForuMe.Services.BlogAPI.Models.Dtos;
using ForuMe.Services.BlogAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ForuMe.Services.BlogAPI.Controllers
{
    [Route("api/articles")]
    public class ArticleAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IArticleRepository _articleRepository;

        public ArticleAPIController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                var articleDtos = await _articleRepository.GetArticles();
                _response.Result = articleDtos;
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
                var articleDtos = await _articleRepository.GetArticleById(id);
                _response.Result = articleDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] ArticleDto articleDto)
        {
            try
            {
                var model = await _articleRepository.CreateUpdateArticle(articleDto);
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
        public async Task<ResponseDto> Put([FromBody] ArticleDto articleDto)
        {
            try
            {
                var model = await _articleRepository.CreateUpdateArticle(articleDto);
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
                var isSuccess = await _articleRepository.DeleteArticle(id);
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
