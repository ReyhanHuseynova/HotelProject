using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
