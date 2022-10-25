using ForuMe.Web.Models;
using ForuMe.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            var response = await _blogService.GetAllBlogsAsync<ResponseDto>();

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
                var response = await _blogService.CreateBlogAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _blogService.GetAllBlogByIdAsync<ResponseDto>(id);
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
                var response = await _blogService.UpdateBlogAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _blogService.GetAllBlogByIdAsync<ResponseDto>(id);
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
                var response = await _blogService.DeleteBlogAsync<ResponseDto>(model.Id);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
