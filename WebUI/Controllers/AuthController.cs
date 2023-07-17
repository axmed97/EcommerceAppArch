using Entities.Concrete;
using Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var checkEmail = await _userManager.FindByEmailAsync(login.Email);

            if(checkEmail == null)
            {
                ModelState.AddModelError("Error", "Error or Password in not valid!");
                return View(login);
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(checkEmail, login.Password, login.RememberMe, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error", "Error or Password in not valid!");
                return View(login);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            var checkEmail = await _userManager.FindByEmailAsync(register.Email);

            if(checkEmail != null)
            {
                ModelState.AddModelError("Error", "Email is exist!:)");
                return View(register);
            }

            User newUser = new()
            {
                Address = "/",
                PhotoUrl = "/",
                Email = register.Email,
                UserName = register.Email,
                Lastname = register.Lastname,
                Firstname = register.Firstname
            };

            var result = await _userManager.CreateAsync(newUser, register.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user: newUser, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Error", error.Description);
                }
                return View(register);
            }

        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
