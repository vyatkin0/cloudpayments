using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreVyatkin.Models
{
    //Интерфейс для работы с заказами
    public interface IReservationRepository
    {
        //Получает список заказов
        IEnumerable<KeyValuePair<int,Reservation>> Reservations { get; }
        
        //Получает заказ из списка с указанным идентификатором
        Reservation this[int id] { get; }

        //Добавляет заказ в список
        int AddReservation(Reservation reservation);

        //Удаляет заказ с указанным идентификатором из списока
        void DeleteReservation(int id);

        //Запрашивает статус оплаты заказа
        Task<ConfirmOfReservation> CheckPayment(string InvoiceId);
    }
}