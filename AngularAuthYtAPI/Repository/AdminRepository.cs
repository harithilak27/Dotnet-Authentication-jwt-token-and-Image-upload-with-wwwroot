using AngularAuthYtAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Nethi.Models;
using Nethi.Global_Exception;
namespace Nethi.Repository
{
    public class AdminRepository : IAdminRepostory
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminRepository(AppDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }

        //post image
        public async Task<ImageFile> PostDashboardImage([FromForm] ImageFile imagefile)
        {
            if (imagefile == null)
            {
                throw new ArgumentException("Invalid file");
            }

            imagefile.ImageName = await SaveImage(imagefile.ProductImage);
            _dbContext.ImageFiles.Add(imagefile);
            await _dbContext.SaveChangesAsync();
            return imagefile;
        }


        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }



        //getimages
        public async Task<List<ImageFile>> GetAllDashboard()
        {
            var item = await _dbContext.ImageFiles.ToListAsync();
            if (item == null)
            {
                throw new Exception(CustomException.ExceptionMessages["CantEmpty"]);
            }
            return item;

        }
    }
}
