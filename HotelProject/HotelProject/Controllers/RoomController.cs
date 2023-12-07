using HotelProject.DAL;
using HotelProjectEntity.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDbContext _db;
        public RoomController(AppDbContext db)
        {
            _db=db;
        }
        public async Task<IActionResult> Index()
        {
            List<Room>? room = await _db.Rooms.Where(p =>!p.IsDeactive).Include(x=>x.Bookings).ToListAsync();
            return View(room);
        }
    }
}
