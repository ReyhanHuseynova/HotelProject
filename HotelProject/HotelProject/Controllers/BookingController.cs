using HotelProject.DAL;
using HotelProject.ViewModels;
using HotelProjectEntity.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _db;
        public BookingController(AppDbContext db)
        {
            _db= db;    
        }
        public async Task<IActionResult> Index()
        {
        
            return View();
        }

        public async Task<IActionResult> Room_Booking()
        {
            ViewBag.Room = await _db.Rooms.Where(x=>!x.IsDeactive).ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Room_Booking(Booking booking, int roomId)
        {
            ViewBag.Room = await _db.Rooms.Where(x=>!x.IsDeactive && booking.RoomId == roomId).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(booking);
            }

            booking.RoomId = roomId;
            await _db.Bookings.AddAsync(booking);
            await _db.SaveChangesAsync();   
            return RedirectToAction("Index");
        }
    }
}
