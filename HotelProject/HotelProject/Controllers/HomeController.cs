using HotelProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelProject.Controllers
{
    public class HomeController : Controller
    {
        
       

        public IActionResult Index()
        {
            return View();
        }

       
    }
}