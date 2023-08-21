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

        public IActionResult Index(List<int> categoryIds, int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.ProductCount = _productService.GetProductCount(3, categoryIds).Data;

            var result = _productService.GetAllFilteredProducts(categoryIds, "az-Az", 0, maxPrice: 10000, pageNo: page, take: 3);
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
        // "1,2,3,45,"
        public JsonResult GetDatas(int page, int take, string categoryList)
        {
            var categories = _categoryService.GetAllFilterCategories("Az");
            var cats = new List<int>();

            if(categoryList == "null")
            {
                cats = categories.Data.Select(x => x.Id).ToList();
            }
            else
            {
                cats = categoryList.Split(",").Select(x => Convert.ToInt32(x)).ToList();
            }

            var result = _productService.GetAllFilteredProducts(cats, "az-Az", 0, maxPrice: 10000, pageNo: page, take: take);
            var productCount = _productService.GetProductCount(take, cats).Data;
            PaginationVM paginationVM = new()
            {
                ProductCount = productCount,
                Products = result.Data
            };
            return Json(paginationVM);
        }
    }
}
