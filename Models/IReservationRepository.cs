using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreVyatkin.Models
{
    public interface IReservationRepository
    {
        IEnumerable<KeyValuePair<int,Reservation>> Reservations { get; }
        
        Reservation this[int id] { get; }
        int AddReservation(Reservation reservation);
        void DeleteReservation(int id);
        Task<ConfirmOfReservation> CheckPayment(string InvoiceId);
    }
}