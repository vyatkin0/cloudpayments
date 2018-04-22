using Microsoft.AspNetCore.Mvc;
using StoreVyatkin.Models;
using System.Linq;

namespace StoreVyatkin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() {
            return View();
        }
    }
}