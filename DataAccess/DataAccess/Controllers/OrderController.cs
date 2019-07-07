using Microsoft.AspNetCore.Mvc;
using Model.Abstractions;
using Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Controllers
{
    /// <summary>
    /// Контроллера для работы с заказами
    /// </summary>
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;

        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        /// <summary>
        /// Получить все заказы
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var orders = _orderRepo.GetAll();
            return Ok(orders);
        }

        /// <summary>
        /// Получить заказ по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orderRepo.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        /// <summary>
        /// Добавить заказ
        /// </summary>
        /// <param name="order">Заказ</param>
        [HttpPut]
        public IActionResult Add([FromBody, Required] Order order)
        {
            _orderRepo.Add(order);
            return Ok(order.Id);
        }

        /// <summary>
        /// Удалить заказ
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderRepo.Remove(id);
            return Ok();
        }
    }
}
