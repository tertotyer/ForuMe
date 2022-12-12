using ForuMe.Web.Models;
using ForuMe.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ForuMe.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = new List<CategoryDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _categoryService.GetAllCategoriesAsync<ResponseDto>(accessToken);

            if (response != null && response.IsSuccess)
            {
                categories = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result));
            }
            return View(categories);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _categoryService.CreateCategoryAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _categoryService.GetCategoryByIdAsync<ResponseDto>(id, accessToken);
            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<BlogDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _categoryService.UpdateCategoryAsync<ResponseDto>(model, accessToken);
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
            var response = await _categoryService.GetCategoryByIdAsync<ResponseDto>(id, accessToken);
            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<BlogDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _categoryService.DeleteCategoryAsync<ResponseDto>(model.Id, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
