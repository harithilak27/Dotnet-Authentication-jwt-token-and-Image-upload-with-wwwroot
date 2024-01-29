using Microsoft.AspNetCore.Mvc;
using Nethi.Models;


namespace Nethi.Repository
{
    public interface IAdminRepostory
    {
        Task<ImageFile> PostDashboardImage([FromForm] ImageFile imageFile);
        Task<List<ImageFile>> GetAllDashboard();
    }
}
