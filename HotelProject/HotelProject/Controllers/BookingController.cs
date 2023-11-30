using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
