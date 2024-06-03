using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Terra.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public ImageController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("UploadFile")]
        public async Task<string> UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string fName = file.FileName;
                string path = Path.Combine(_environment.ContentRootPath, "Images", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return $"{file.FileName} successfully uploaded to the Server";
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpGet()]
        public async Task<IActionResult> GetAllImages()
        {
            var imageDirectory = Path.Combine(_environment.ContentRootPath, "Images");
            List<FileContentResult> images = new List<FileContentResult>();

            string[] imagePaths = Directory.GetFiles(imageDirectory);

            foreach (var imagePath in imagePaths)
            {
                Byte[] b = await System.IO.File.ReadAllBytesAsync(imagePath);
                var splitedPath = imagePath.Split('\\');
                var fileName = splitedPath[splitedPath.Length - 1];
                var file = File(b, "image/jpeg", fileName);
                images.Add(file);
            }

            return Ok(images);
        }
    }
}
