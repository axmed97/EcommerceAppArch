using Business.Abstract;
using Entities.DTOs.CartDTOs;
using Entities.DTOs.OrderDTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IOrderService _orderService;

        public CartController(IProductService productService, IHttpContextAccessor contextAccessor, IUserService userService, IOrderService orderService)
        {
            _productService = productService;
            _contextAccessor = contextAccessor;
            _userService = userService;
            _orderService = orderService;
        }

        public JsonResult AddToCart(string id, int quantity)
        {
            var cookie = Request.Cookies["cart"];

            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            cookieOptions.Path = "/";

            List<CartCookieDTO> cartCookies = new();

            CartCookieDTO cartCookieDTO = new()
            {
                Id = Convert.ToInt32(id),
                Quantity = quantity
            };
            if(cookie == null)
            {
                cartCookies.Add(cartCookieDTO);
                var cookieJson = JsonSerializer.Serialize<List<CartCookieDTO>>(cartCookies);
                Response.Cookies.Append("cart", cookieJson, cookieOptions);
            }
            else
            {
                var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
                var findData = data.FirstOrDefault(x => x.Id == Convert.ToInt32(id));

                if(findData != null)
                {
                    findData.Quantity += 1;
                }
                else
                {
                    data.Add(cartCookieDTO);
                }
                var cookieJson = JsonSerializer.Serialize<List<CartCookieDTO>>(data);
                Response.Cookies.Append("cart", cookieJson, cookieOptions);
            }
            return Json("");
        }

        public IActionResult UserCart()
        {
            return View();
        }

        public JsonResult GetProduct()
        {
            var cookie = Request.Cookies["cart"];
            var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
            var quantity = data.Select(x => x.Quantity).ToList();
            var dataIds = data.Select(x => x.Id).ToList();
            var result = _productService.GetProductForCart(dataIds, "az-Az", quantity);
            return Json(result);
        }

        public JsonResult RemoveData(string id)
        {
            var cookie = Request.Cookies["cart"];
            var cookieOptions = new CookieOptions();
            //cookieOptions.Expires = DateTime.Now.AddDays(1);
            //cookieOptions.Path = "/";

            var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
            var result = data.FirstOrDefault(x => x.Id == Convert.ToInt32(id));

            data.Remove(result);

            var cookieJson = JsonSerializer.Serialize<List<CartCookieDTO>>(data);
            Response.Cookies.Append("cart", cookieJson, cookieOptions);

            return Json("ok");
        }

        public IActionResult Checkout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }

            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserById(userId);

            var cookie = Request.Cookies["cart"];
            var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
            var quantity = data.Select(x => x.Quantity).ToList();
            var dataIds = data.Select(x => x.Id).ToList();
            var result = _productService.GetProductForCart(dataIds, "az-Az", quantity);

            CheckoutVM vm = new()
            {
                CartItems = result.Data,
                User = user.Data
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Checkout(string id)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _userService.GetUserById(userId);

            var cookie = Request.Cookies["cart"];
            var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
            var quantity = data.Select(x => x.Quantity).ToList();
            var dataIds = data.Select(x => x.Id).ToList();
            var result = _productService.GetProductForCart(dataIds, "az-Az", quantity);

            List<OrderCreateDTO> orderList = new();

            foreach (var item in result.Data)
            {
                OrderCreateDTO orderCreateDTO = new()
                {
                    UserId = userId,
                    ProductId = item.Id,
                    ProductPrice = item.Price,
                    Quantity = item.Quantity,
                    Message = "Yoxdu"
                };
                orderList.Add(orderCreateDTO);
            }

            _orderService.CreateOrder(orderList);

            return RedirectToAction("Index", "Home");
        }
    }
}
