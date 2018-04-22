using Microsoft.AspNetCore.Mvc;
using System.Linq;
using StoreVyatkin.Models;

namespace StoreVyatkin.Controllers
{
    // Класс объекта контроллера списка товаров
    [Route("/[controller]")]
    public class ProductsController : Controller
    {
        //Интерфейс для доступа к списку товаров
        private IProductRepository repository;
        public ProductsController(IProductRepository repo)
        {
            repository = repo;
        }

        // Генерирует список товаров
        [HttpGet]
        public IQueryable<Product> List() => repository.Products;
    }
}