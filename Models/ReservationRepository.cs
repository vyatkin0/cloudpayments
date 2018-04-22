using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreVyatkin.Models
{
    public class ReservationRepository : IReservationRepository
    {
        private Dictionary<int, Reservation> items;
        public ReservationRepository()
        {
            items = new Dictionary<int, Reservation>();
        }

        public Reservation this[int id] => items.ContainsKey(id) ? items[id] : null;
        public IEnumerable<KeyValuePair<int,Reservation>> Reservations => items;
        public int AddReservation(Reservation reservation)
        {
            int ReservationId = new System.Random().Next();
            items[ReservationId] = reservation;
            return ReservationId;
        }
        public void DeleteReservation(int id) => items.Remove(id);
        public Task<ConfirmOfReservation> CheckPayment(string InvoiceId)
        {
            /*
            Здесь необходимо произвести проверку статуса платежа используя запрос к серверу api.cloudpayments.ru
            Запрос требует наличия личного кабинета.
            HttpClient client = new HttpClient();
            Task<string> stringTask = client.GetStringAsync("https://api.cloudpayments.ru/payments/find");
            string rawJsonString = await stringTask;
            */
            
            //Просто всегда возвращаем успешный результат проверки
            return Task<ConfirmOfReservation>.Run(() => new ConfirmOfReservation(){Success = true, Message = "Success"});
        }
    }
}