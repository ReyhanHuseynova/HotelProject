using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
