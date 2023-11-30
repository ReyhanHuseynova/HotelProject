using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
