using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Model.Abstractions;
using Model.Entities;

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

        [HttpPut]
        public IActionResult Add([FromBody, Required] Customer customer)
        {
            _customerRepo.Add(customer);
            return Ok(customer.Id);
        }
    }
}
