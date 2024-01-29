using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Nethi.Global_Exception;
using Nethi.Models;
using Nethi.Repository;

namespace Nethi.Services
{
    public class AdminServices:IAdminServices
    {
        private readonly IAdminRepostory _adminRepo;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminServices(IAdminRepostory adminRepo, IWebHostEnvironment hostEnvironment)
        {
            _adminRepo = adminRepo;
            _hostEnvironment = hostEnvironment;
        }

        //post image
        public async Task<ImageFile> PostDashboardImage([FromForm] ImageFile imageFile)
        {
            if (imageFile == null)
            {
                throw new Exception("Invalid file");
            }
            var item = await _adminRepo.PostDashboardImage(imageFile);
            if (item == null)
            {
                throw new Exception(CustomException.ExceptionMessages["CantEmpty"]);
            }
            return item;
        }


        //get images
        public async Task<List<ImageFile>> GetAllDashboard()
        {
            var get = await _adminRepo.GetAllDashboard();
            var imageList = new List<ImageFile>();
            foreach (var image in get)
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                var filePath = Path.Combine(uploadsFolder, image.ImageName);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                var tourData = new ImageFile
                {
                    Id = image.Id,
                    Name = image.Name,
                    Description = image.Description,
                    ImageSrc = image.ImageSrc,
                    ImageName = Convert.ToBase64String(imageBytes)
                };
                imageList.Add(tourData);
            }
            return imageList;
        }
    }
}
