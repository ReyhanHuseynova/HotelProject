using HotelProject.DAL;
using HotelProjectEntity.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HotelProject.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _db;
        public TeamController(AppDbContext db)
        {
            _db=db;
        }
        public async Task<IActionResult> Index()
        {
            List<Team> teams = await _db.Teams.Include(x=>x.Position).ToListAsync();
            return View(teams);
        }
    }
}
