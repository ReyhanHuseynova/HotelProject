using HotelProject.DAL;
using HotelProjectEntity.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _db;
        public PositionController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Position> teams = await _db.Positions.ToListAsync();
            return View(teams);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Position p)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _db.Positions.AddAsync(p);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int ?id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null)
            {
                return NotFound();
            }
            Position ?dbposition=await _db.Positions.FirstOrDefaultAsync(x=>x.Id== id); 
            if(dbposition == null)
            {
                return BadRequest();
            }
            return View(dbposition);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Position p, int? id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            if (id == null)
            {
                return NotFound();
            }
            Position? dbposition = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (dbposition == null)
            {
                return BadRequest();
            }
            dbposition.Name = p.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
            {
                return NotFound();
            }

            Position ?dbposition=await _db.Positions.FirstOrDefaultAsync(x=>x.Id== id);  
            
            if(dbposition == null)
            {
                return BadRequest();
            }
            if(dbposition.IsDeactive)
            {
                dbposition.IsDeactive = false;
            }
            else
            {
                dbposition.IsDeactive = true;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}

