using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Model.Abstractions;
using Model.Entities;

namespace DataAccess.Controllers
{
    /// <summary>
    /// Контроллер для работы с продуктами
    /// </summary>
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _productRepo;

        public ProductController(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _productRepo.GetAll();
            return Ok(products);
        }

        [HttpPut]
        public IActionResult Add([FromBody, Required] Product product)
        {
            _productRepo.Add(product);
            return Ok(product.Id);
        }
    }
}
