using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
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
