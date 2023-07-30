using Business.Abstract;
using Entities.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, IHttpContextAccessor contextAccessor, ICategoryService categoryService)
        {
            _productService = productService;
            _contextAccessor = contextAccessor;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _productService.GetDashboardProducts(userId, "az-Az");
            return View(result.Data);
        }

        public IActionResult Create()
        {
            var categories = _categoryService.GetAllCategories("Eng");
            ViewBag.Categories = new SelectList(categories.Data, "Id", "CategoryName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductAddDTO productAdd)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _productService.AddProductAdmin(userId, productAdd);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
