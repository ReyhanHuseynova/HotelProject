using Microsoft.AspNetCore.Mvc;

namespace HotelProject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
