using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult Change(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });


            CookieOptions cookieOptions = new();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("Ecommerce", culture, cookieOptions);

            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult Index(string id, string title)
        {
            var currentURL = Url.PageLink();
            if (id != "Az" && id != "Ru" && id != "Eng")
            {
                return NotFound();
            }
            CookieOptions cookieOptions = new();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("Ecommerce", id, cookieOptions);
            return LocalRedirect(title);
        }
    }
}
