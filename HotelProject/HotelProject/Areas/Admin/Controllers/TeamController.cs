using HotelProject.Controllers;
using HotelProject.DAL;
using HotelProject.Helpers;
using HotelProjectEntity.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public TeamController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            List<Team> teams = await _db.Teams.Include(x=>x.Social).Include(p=>p.Position)
                .ToListAsync();
            return View(teams);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Position = await _db.Positions.ToListAsync();
            ViewBag.Social = await _db.Socials.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Team team, int posId,int socialId)
        {
            ViewBag.Position=await _db.Positions.ToListAsync();
            ViewBag.Social=await _db.Socials.ToListAsync();

            if(!ModelState.IsValid)
            {
                return View(team);
            }
            if (team.Photo == null)
            {
                ModelState.AddModelError("Photo", "Select photo");
                return View(team);
            }
            if (!team.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Select photo format");
                return View(team);
            }
            if (team.Photo.IsOlder2Mb())
            {
                ModelState.AddModelError("Photo", "Max 2Mb");
                return View(team);
            }
            string folder = Path.Combine(_env.WebRootPath, "hotel", "img");
            team.Image = await team.Photo.SaveImageAsync(folder);

            team.PositionId= posId;    
            team.SocialId= socialId;    
            team.CreateDate = DateTime.UtcNow.AddHours(4);

            await _db.Teams.AddAsync(team);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Position = await _db.Positions.ToListAsync();
            ViewBag.Social = await _db.Socials.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }
            Team ?dbTeam= await _db.Teams.FirstOrDefaultAsync(x=>x.Id==id);
            if(dbTeam == null)
            {
                return BadRequest();  
            }
            return View(dbTeam);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Team team,int? id,int posId, int socialId)
        {
            ViewBag.Position = await _db.Positions.ToListAsync();
            ViewBag.Social = await _db.Socials.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }
            Team? dbTeam = await _db.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (dbTeam == null)
            {
                return BadRequest();
            }
            if (team.Photo != null)
            {
                if (!team.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo format");
                    return View();
                }
                if (team.Photo.IsOlder2Mb())
                {
                    ModelState.AddModelError("Photo", "Max 2Mb");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "hotel","img");
                string path = Path.Combine(folder, dbTeam.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbTeam.Image = await team.Photo.SaveImageAsync(folder);
            }
            dbTeam.PositionId= posId;
            dbTeam.SocialId = socialId;
            dbTeam.FullName= team.FullName; 
            dbTeam.UpdateDate= team.UpdateDate; 

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Team ?dbTeam= await _db.Teams.FirstOrDefaultAsync(x=>x.Id==id);
            if(dbTeam == null)
            {
                return BadRequest();
            }
            if(dbTeam.IsDeactive)
            {
                dbTeam.IsDeactive = false;
            }
            else
            {
                dbTeam.IsDeactive = true;   
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
