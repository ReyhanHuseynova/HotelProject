using HotelProject.DAL;
using HotelProject.Helpers;
using HotelProjectEntity.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public RoomController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            List<Room>rooms= await _db.Rooms.ToListAsync(); 
            return View(rooms);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {


            if (!ModelState.IsValid)
            {
                return View(room);
            }
            if (room.Photo == null)
            {
                ModelState.AddModelError("Photo", "Select photo");
                return View(room);
            }
            if (!room.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Select photo format");
                return View(room);
            }
            if (room.Photo.IsOlder2Mb())
            {
                ModelState.AddModelError("Photo", "Max 2Mb");
                return View(room);
            }
            string folder = Path.Combine(_env.WebRootPath, "hotel", "img");
            room.Image = await room.Photo.SaveImageAsync(folder);

            room.CreateDate = DateTime.UtcNow.AddHours(4);
            await _db.Rooms.AddAsync(room);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }
            Room ?dbRoom = await _db.Rooms.FirstOrDefaultAsync(x=>x.Id==id);
            if(dbRoom == null)
            {
                return BadRequest();
            }
            return View(dbRoom);

        }
        [HttpPost]
        public async Task<IActionResult> Update(Room room,int? id)
        {
            if(!ModelState.IsValid)
            {
                return View();  
            }
            if (id == null)
            {
                return NotFound();
            }
            Room? dbRoom = await _db.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRoom == null)
            {
                return BadRequest();
            }

            if (room.Photo != null)
            {
                if (!room.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo format");
                    return View();
                }
                if (room.Photo.IsOlder2Mb())
                {
                    ModelState.AddModelError("Photo", "Max 2Mb");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "hotel", "img");
                string path = Path.Combine(folder, dbRoom.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbRoom.Image = await room.Photo.SaveImageAsync(folder);
            }
            dbRoom.Name = room.Name;
            dbRoom.Description = room.Description;
            dbRoom.Price = room.Price;
            dbRoom.BathCount = room.BathCount;
            dbRoom.BedCount = room.BedCount;
            dbRoom.Count = room.Count;
            dbRoom.UpdateDate = room.UpdateDate;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int ?id)
        {
            if(id== null) 
            {
                return NotFound();
            }

            Room ?dbRoom = await _db.Rooms.FirstOrDefaultAsync(x=>x.Id==id);
            if(dbRoom == null)
            {
                return BadRequest();
            }

            if(dbRoom.IsDeactive)
            {
                dbRoom.IsDeactive = false;
            }
            else
            {
                dbRoom.IsDeactive=true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
