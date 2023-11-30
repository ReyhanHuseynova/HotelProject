using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
