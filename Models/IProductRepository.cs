using System.Linq;

namespace StoreVyatkin.Models
{
    //Интерфейс для доступа к списку товаров
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}