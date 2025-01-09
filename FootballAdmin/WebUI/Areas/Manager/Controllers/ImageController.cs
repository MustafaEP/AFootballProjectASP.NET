using Business.Abstract.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Manager.Controllers
{
    [Area("Manager"), Authorize(Roles = "Manager,Admin")]
    public class ImageController : Controller
    {
        private readonly IImageSearchService _imageSearchService;

        public ImageController(IImageSearchService imageSearchService)
        {
            _imageSearchService = imageSearchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayerImage(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                return BadRequest("Player name is required.");
            }

            string imageUrl = await _imageSearchService.GetPlayerImageAsync(playerName);
            return Json(new { imageUrl });
        }
    }
}
