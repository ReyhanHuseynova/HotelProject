using HotelProject.DAL;
using HotelProjectEntity.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Controllers
{
    public class ServiceController : Controller
    {
        private readonly AppDbContext _db;
        public ServiceController(AppDbContext db)
        {
            _db = db; 
        }
        public async Task<IActionResult> Index()
        {
            List<Service>service=await _db.Services.ToListAsync();  
            return View(service);
        }
    }
}
