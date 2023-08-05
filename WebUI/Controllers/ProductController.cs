using Business.Abstract;
using Entities.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.test = _categoryService.GetAllCategories();
            ViewBag.CurrentPage = page;
            ViewBag.ProductCount = _productService.GetProductCount(3);
            var result = _productService.GetAllFilteredProducts("az-Az", 0, maxPrice: 10000, pageNo: page, take: 3);
            var categories = _categoryService.GetAllFilterCategories("Az");
            ProductFilterVM productFilterVM = new()
            {
                ProductFiltereds = result.Data,
                CategoryFilters = categories.Data
            };
            return View(productFilterVM);
        }

        public IActionResult Detail(int id)
        {
            var result = _productService.GetProductById(id, "az-Az");
            if (result.Success)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
