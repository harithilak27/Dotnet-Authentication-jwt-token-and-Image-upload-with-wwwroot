using AngularAuthYtAPI.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nethi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Nethi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
       private readonly AppDbContext _dbContext;
        public ImageUploadController(IWebHostEnvironment webHostEnvironment, AppDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<string> Post([FromForm] ImageFile imageFile)
        {
            try
            {
                if (imageFile.image.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath+ "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + imageFile.image.FileName))
                    {
                        imageFile.image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    await _dbContext.AddAsync(imageFile);
                    await _dbContext.SaveChangesAsync();
                    return "uploaded";
                }
                else
                {
                    return "failed.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

      
        [HttpGet("{FileName}")]
        public async Task<IActionResult> Get([FromRoute] string FileName)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
            var filePath = path + FileName + ".png";
            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                return File(b, "image/png");
            }
            return null;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";

            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path, "*.png");
                List<string> fileNames = new List<string>();

                foreach (var file in files)
                {
                    fileNames.Add(Path.GetFileNameWithoutExtension(file));
                }

                return Ok(fileNames);
            }
            else
            {
                return NotFound("Uploads directory not found");
            }
        }
    }
}
