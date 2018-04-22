namespace StoreVyatkin.Models
{
    public class ConfirmOfPayment
    {
        public decimal Amount {get; set;}
        public bool Auth {get; set;}
        public string Currency {get; set;}
        public string Description {get; set;}
        public string InvoiceId {get; set;}
        public string PublicId {get; set;}           
    }

    public class ConfirmOfReservation
    {
        public bool Success {get; set;}
        public string Message {get; set;}           
    }

    public class Reservation
    {
        public int[] Products { get; set; }
        public ConfirmOfPayment Confirmation {get; set;}
    }
}