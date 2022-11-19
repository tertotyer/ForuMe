using ForuMe.Web.Models;
using ForuMe.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace ForuMe.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var products = new List<BlogDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _blogService.GetAllBlogsAsync<ResponseDto>(accessToken);

            if (response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<BlogDto>>(Convert.ToString(response.Result));
            }
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _blogService.CreateBlogAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _blogService.GetAllBlogByIdAsync<ResponseDto>(id, accessToken);
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

        public async Task<IActionResult> Delete(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _blogService.GetAllBlogByIdAsync<ResponseDto>(id, accessToken);
            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<BlogDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
