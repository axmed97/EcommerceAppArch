using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class PictureController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public PictureController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<JsonResult> UploadPictures(List<IFormFile> photoUrls)
        {
            List<string> pictures = new();
            for (int i = 0; i < photoUrls.Count; i++)
            {
                string path = "/uploads/" + Guid.NewGuid() + photoUrls[i].FileName;
                using FileStream fileStream = new(_env.WebRootPath + path, FileMode.Create);
                await photoUrls[i].CopyToAsync(fileStream);

                pictures.Add(path);
            }

            return Json(pictures);
        }
    }
}
