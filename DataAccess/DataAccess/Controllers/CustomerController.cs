using Microsoft.AspNetCore.Mvc;
using Model.Abstractions;
using Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Controllers
{
    /// <summary>
    /// Контроллер для работы с покупателями
    /// </summary>
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepo;

        public CustomerController(IRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerRepo.GetAll();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var customer = _customerRepo.GetById(id);

            if (customer == null)
            {
                return BadRequest("Полупатель отсутствует");
            }

            return Ok(customer);
        }

        [HttpPut]
        public IActionResult Add([FromBody, Required] Customer customer)
        {
            _customerRepo.Add(customer);
            return Ok(customer.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _customerRepo.Remove(id);
            return Ok();
        }
    }
}
