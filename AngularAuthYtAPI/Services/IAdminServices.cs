using Microsoft.AspNetCore.Mvc;
using Nethi.Models;

namespace Nethi.Services
{
    public interface IAdminServices
    {
        Task<ImageFile> PostDashboardImage([FromForm] ImageFile imageFile);
        Task<List<ImageFile>> GetAllDashboard();
    }
}
