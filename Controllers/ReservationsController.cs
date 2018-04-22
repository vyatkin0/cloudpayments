using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StoreVyatkin.Models;
using System.Threading.Tasks;

namespace StoreVyatkin.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private IReservationRepository repository;
        public ReservationsController(IReservationRepository repo) => repository = repo;

        [HttpGet]
        public IEnumerable<KeyValuePair<int,Reservation>> Get() => repository.Reservations;

        [HttpPost]
        public int Post([FromBody] int[] basket)
        {
            //Проверка наличия указанных товаров в требуемом количестве,
            //если товары присутствуют, то создается резерв
            Reservation r = new Reservation(){Products = basket};
            return repository.AddReservation(r);
        }

        [HttpPut("{id}")]
        public async Task<ConfirmOfReservation> Put(int id , [FromBody] ConfirmOfPayment confirm)
        {
            Reservation r = repository[id];

            if(null==r)
            {
                return new ConfirmOfReservation(){Success=false, Message="No reservation"};
            }

            r.Confirmation = confirm;

            return await repository.CheckPayment(confirm.InvoiceId);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            //Отмена резерва
            repository.DeleteReservation(id);
            return true;
        }
    }
}