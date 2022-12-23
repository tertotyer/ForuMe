using ForuMe.Services.BlogAPI.Models.Dtos;
using ForuMe.Web.Models;
using ForuMe.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ForuMe.Web.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;

        public ArticlesController(IArticleService articleService, IUserService userService)
        {
            _articleService = articleService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var articles = new List<ArticleDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _articleService.GetAllArticlesAsync<ResponseDto>(accessToken);

            if (response != null && response.IsSuccess)
            {
                articles = JsonConvert.DeserializeObject<List<ArticleDto>>(Convert.ToString(response.Result));
            }
            return View(articles);
        }

        public async Task<IActionResult> ShowArticlesByBlogId(int id)
        {
            var articles = new List<ArticleDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _articleService.GetAllArticlesAsync<ResponseDto>(accessToken);

            if (response != null && response.IsSuccess)
            {
                articles = JsonConvert.DeserializeObject<List<ArticleDto>>(Convert.ToString(response.Result));
            }
            return View("Index", articles.Where(x => x.BlogId == id));
        }

        [Authorize]
        public IActionResult Create(int blogId)
        {
            ViewData["BlogId"] = blogId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _articleService.CreateArticleAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    await _userService.UpdateUserLevelAsync<ResponseDto>(new
                    {
                        Id = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value,
                        Exp = 0.1
                    });
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _articleService.GetArticleByIdAsync<ResponseDto>(id, accessToken);
            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<ArticleDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArticleDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _articleService.UpdateArticleAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _articleService.GetArticleByIdAsync<ResponseDto>(id, accessToken);
            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<ArticleDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ArticleDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _articleService.DeleteArticleAsync<ResponseDto>(model.Id, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
