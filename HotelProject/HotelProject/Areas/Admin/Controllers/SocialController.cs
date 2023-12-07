using HotelProject.DAL;
using HotelProjectEntity.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialController : Controller
    {
        private readonly AppDbContext _db;
        public SocialController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Social>s= await _db.Socials.ToListAsync();
            return View(s);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Social social)
        {
            if (!ModelState.IsValid)
            {
                return View(social);
            }
            //bool isExist = await _db.Services.AnyAsync(x => x.ServiceTitle == social.ServiceTitle && x.Id != service.Id);
            //if (isExist)
            //{
            //    ModelState.AddModelError("ServiceTitle", "This service already exist!");
            //    return View();
            //}

            social.CreateDate = DateTime.UtcNow.AddHours(4);

            await _db.Socials.AddAsync(social);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Social? dbSocial = await _db.Socials.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSocial == null)
            {
                return BadRequest();
            }
            return View(dbSocial);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Social social, int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Social? dbSocial = await _db.Socials.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSocial == null)
            {
                return BadRequest();
            }
            dbSocial.Name = social.Name;
            dbSocial.UpdateDate = social.UpdateDate;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Social? dbSocial = await _db.Socials.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSocial == null)
            {
                return BadRequest();
            }

            if (dbSocial.IsDeactive)
            {
                dbSocial.IsDeactive = false;
            }
            else
            {
                dbSocial.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
