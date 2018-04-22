namespace StoreVyatkin.Models
{
    //Класс объекта подтверждение оплаты
    public class ConfirmOfPayment
    {
        public decimal Amount {get; set;}
        public bool Auth {get; set;}
        public string Currency {get; set;}
        public string Description {get; set;}
        public string InvoiceId {get; set;}
        public string PublicId {get; set;}           
    }

    //Класс объекта подтверждение заказа
    public class ConfirmOfReservation
    {
        public bool Success {get; set;}
        public string Message {get; set;}           
    }

    //Класс объекта заказ
    public class Reservation
    {
        //Массив идентификаторов товаров в заказе
        //Идентификаторы могут дублироваться,
        //если добавлено несколько одинаковых товаров
        public int[] Products { get; set; }
        //Подтверждение оплаты данного заказа
        public ConfirmOfPayment Confirmation {get; set;}
    }
}