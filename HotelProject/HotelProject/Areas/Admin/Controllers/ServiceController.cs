using HotelProject.DAL;
using HotelProjectEntity.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _db;
        public ServiceController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> service = await _db.Services.ToListAsync();
            return View(service);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.Services.AnyAsync(x=>x.ServiceTitle==service.ServiceTitle && x.Id!=service.Id);
            if(isExist)
            {
                ModelState.AddModelError("ServiceTitle", "This service already exist!");
                return View();
            }
            service.CreateDate = DateTime.UtcNow.AddHours(4);

            await _db.Services.AddAsync(service);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int ?id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Service ?dbService= await _db.Services.FirstOrDefaultAsync(x=>x.Id==id);
            if(dbService == null)
            {
                return BadRequest();
            }
            return View(dbService);
        }

        public async Task<IActionResult> Update(Service service,int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Service? dbService = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (dbService == null)
            {
                return BadRequest();
            }
            dbService.ServiceTitle = service.ServiceTitle;  
            dbService.Icon = service.Icon;  
            dbService.Description = service.Description;  
            dbService.UpdateDate = service.UpdateDate;  
            
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int ?id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Service ?dbService= await _db.Services.FirstOrDefaultAsync(x=>x.Id==id);
            if( dbService == null)
            {
                return BadRequest();
            }

            if(dbService.IsDeactive)
            {
                dbService.IsDeactive = false;
            }
            else
            {
                dbService.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
