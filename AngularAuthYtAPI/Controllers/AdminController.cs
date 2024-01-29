using AngularAuthYtAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nethi.Models;
using Nethi.Services;

namespace Nethi.Controllers
{
    [Route("AdminSide")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly AppDbContext _dbContext;

        public AdminController(IAdminServices context, IWebHostEnvironment webHostEnvironment, AppDbContext dbContext)
        {
            _context = context;
            _hostEnvironment = webHostEnvironment;
            _dbContext = dbContext; 
        }

        //post Images
        [HttpPost("UploadImage")]
        public async Task<ActionResult<ImageFile>> PostDashboardImage([FromForm] ImageFile imageFile)
        {
            try
            {
                var createProduct = await _context.PostDashboardImage(imageFile);
                return Ok(createProduct);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //get images 
        [HttpGet("GetProductImage")]
        public async Task<ActionResult<List<ImageFile>>> GetAllDashboard()
        {
            try
            {
                var createProduct = await _context.GetAllDashboard();
                return Ok(createProduct);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
