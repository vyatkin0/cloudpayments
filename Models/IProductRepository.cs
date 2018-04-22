using System.Linq;

namespace StoreVyatkin.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}