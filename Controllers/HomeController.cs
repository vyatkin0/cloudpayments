using Microsoft.AspNetCore.Mvc;

namespace StoreVyatkin.Controllers
{
    // Класс объекта контроллера домашней страницы
    public class HomeController : Controller
    {
        // Генерирует домашнюю страницу /Home/Index
        public IActionResult Index() {
            return View();
        }
    }
}