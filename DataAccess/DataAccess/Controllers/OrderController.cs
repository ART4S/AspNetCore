using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Model.Abstractions;
using Model.Entities;

namespace DataAccess.Controllers
{
    /// <summary>
    /// Контроллера для работы с заказами
    /// </summary>
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepo;

        public OrderController(IRepository<Order> orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var orders = _orderRepo.GetAll();
            return Ok(orders);
        }

        [HttpPut]
        public IActionResult Add([FromBody, Required] Order order)
        {
            _orderRepo.Add(order);
            return Ok(order.Id);
        }
    }
}
