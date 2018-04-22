using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreVyatkin.Models
{
    //Класс объекта БД товар
    public class Product
    {
        //Идентификатор товара
        public int ProductID { get; set; }
        //Название товара
        //[MaxLength(300)] 
        public string Name { get; set; }
        //Стоимость товара
        public decimal Price { get; set; }
        //Валюта стоимости товара
        [Column("Currency", TypeName="char(3)")]
        public string Currency { get; set; }
    }
}