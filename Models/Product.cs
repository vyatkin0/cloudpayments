using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreVyatkin.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        //[MaxLength(300)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Column("Currency", TypeName="char(3)")]
        public string Currency { get; set; }
    }
}