using System.Linq;

namespace StoreVyatkin.Models
{
    //Класс объекта получающего список товаров из БД
    public class EFProductRepository : IProductRepository
    {
        //Контекст подключения к БД
        private ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        //Список товаров
        public IQueryable<Product> Products => context.Products;
    }
}