using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StoreVyatkin.Models;

namespace StoreVyatkin.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        //Интерфейс для доступа к списку заказов
        private IReservationRepository repository;
        public ReservationsController(IReservationRepository repo) => repository = repo;

        /// <summary>
        /// Генерирует список заказов
        /// </summary>
        /// <returns>Список заказов</returns>
        [HttpGet]
        public IEnumerable<KeyValuePair<int,Reservation>> Get() => repository.Reservations;

        /// <summary>
        /// Резервирует товары и формирует заказ
        /// </summary>
        /// <param name="basket">Массив идентификаторов товаров в заказе</param>
        /// <returns>Идентификатор заказа</returns>
        [HttpPost]
        public int Post([FromBody] int[] basket)
        {
            //Проверка наличия указанных товаров в требуемом количестве,
            //если товары присутствуют, то создается резерв
            Reservation r = new Reservation(){Products = basket};
            return repository.AddReservation(r);
        }

        /// <summary>
        /// Принимает подтверждение оплаты заказа с указанным идентификатором и подтверждает заказ
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="confirm">Подтвержедние оплаты заказа</param>
        /// <returns>Объект - подтверждение заказа</returns>
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


        /// <summary>
        /// Удаляет заказ с указанным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //Отмена резерва
            repository.DeleteReservation(id);
        }
    }
}