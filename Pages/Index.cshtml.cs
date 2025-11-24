using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private HttpClient _httpClient;
        private Options _options;

        public IndexModel(HttpClient httpClient, Options options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        [BindProperty]
        public List<string> ImageList { get; private set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task OnGetAsync()
        {
            var imagesUrl = _options.ApiUrl;

            string imagesJson = await _httpClient.GetStringAsync(imagesUrl);

            IEnumerable<string> imagesList = JsonConvert.DeserializeObject<IEnumerable<string>>(imagesJson);

            this.ImageList = imagesList.ToList<string>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Upload != null && Upload.Length > 0)
            {
                // Max file size: 10 MB
                const long MaxFileSizeBytes = 10 * 1024 * 1024; // 10 MB
                if (Upload.Length > MaxFileSizeBytes)
                {
                    ModelState.AddModelError("Upload", "ไม่ได้นะ ต้องอัพโหลดไฟล์น้อยกว่า 10MB");
                    return Page();
                }

                // Extension whitelist only
                var ext = System.IO.Path.GetExtension(Upload.FileName ?? string.Empty).ToLowerInvariant();
                var allowedExt = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
                if (!allowedExt.Contains(ext))
                {
                    ModelState.AddModelError("Upload", "Only image files are allowed.");
                    return Page();
                }

                var imagesUrl = _options.ApiUrl;
                using (var stream = Upload.OpenReadStream())
                using (var image = new StreamContent(stream))
                {
                    if (!string.IsNullOrEmpty(Upload.ContentType))
                    {
                        image.Headers.ContentType = new MediaTypeHeaderValue(Upload.ContentType);
                    }

                    var response = await _httpClient.PostAsync(imagesUrl, image);
                    // Optionally handle response here
                }
            }

            return RedirectToPage("/Index");
        }
    }
}