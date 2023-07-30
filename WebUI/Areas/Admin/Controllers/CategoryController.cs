using Business.Abstract;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAdminCategories("Az");
            if (result.Success)
            {
                return View(result.Data);
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryAddDTO categoyAddDTO)
        {
            _categoryService.AddCategory(categoyAddDTO);
            return RedirectToAction("Index");
        }
    }
}
