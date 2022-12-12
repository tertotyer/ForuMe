using ForuMe.Web.Models;
using ForuMe.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace ForuMe.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public BlogsController(IBlogService blogService, ICategoryService categoryService, IUserService userService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = new List<BlogDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _blogService.GetAllBlogsAsync<ResponseDto>(accessToken);

            if (response != null && response.IsSuccess)
            {
                blogs = JsonConvert.DeserializeObject<List<BlogDto>>(Convert.ToString(response.Result));
            }
            return View(blogs);
        }

        public async Task<IActionResult> ShowBlogsByCategoryId(int id)
        {
            var blogs = new List<BlogDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _blogService.GetAllBlogsAsync<ResponseDto>(accessToken);

            if (response != null && response.IsSuccess)
            {
                blogs = JsonConvert.DeserializeObject<List<BlogDto>>(Convert.ToString(response.Result));
            }
            return View("Index", blogs.Where(x => x.CategoryId == id));
        }

        [Authorize]
        public async Task<IActionResult> UserBlogs()
        {
            var blogs = new List<BlogDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");  
            var response = await _blogService.GetAllBlogsAsync<ResponseDto>(accessToken);

            if (response != null && response.IsSuccess)
            {
                blogs = JsonConvert.DeserializeObject<List<BlogDto>>(Convert.ToString(response.Result));
            }
            return View("Index", blogs.Where(x => x.Author == User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value));
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categories = new List<CategoryDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _categoryService.GetAllCategoriesAsync<ResponseDto>(accessToken);

            if (response != null && response.IsSuccess)
            {
                categories = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result));
            }

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogDto model)
        {
            model.Author = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _blogService.CreateBlogAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    await _userService.UpdateUserLevel<ResponseDto>(0.1);
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _blogService.GetBlogByIdAsync<ResponseDto>(id, accessToken);
            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<BlogDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _blogService.UpdateBlogAsync<ResponseDto>(model, accessToken);
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
            var response = await _blogService.GetBlogByIdAsync<ResponseDto>(id, accessToken);
            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<BlogDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(BlogDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _blogService.DeleteBlogAsync<ResponseDto>(model.Id, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
