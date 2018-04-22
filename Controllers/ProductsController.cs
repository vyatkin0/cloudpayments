using Microsoft.AspNetCore.Mvc;
using StoreVyatkin.Models;
using System.Linq;

namespace StoreVyatkin.Controllers
{
    [Route("/[controller]")]
    public class ProductsController : Controller
    {
        private IProductRepository repository;
        public ProductsController(IProductRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public IQueryable<Product> List() => repository.Products;
    }
}