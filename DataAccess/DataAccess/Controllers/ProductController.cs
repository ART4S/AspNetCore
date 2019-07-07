using Microsoft.AspNetCore.Mvc;
using Model.Abstractions;
using Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Controllers
{
    /// <summary>
    /// Контроллер для работы с продуктами
    /// </summary>
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        /// <summary>
        /// Получить все продукты
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var products = _productRepo.GetAll();
            return Ok(products);
        }

        /// <summary>
        /// Получить продукт по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор продукта</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productRepo.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Добавить продукт
        /// </summary>
        /// <param name="product">Продукт</param>
        [HttpPut]
        public IActionResult Add([FromBody, Required] Product product)
        {
            _productRepo.Add(product);
            return Ok(product.Id);
        }

        /// <summary>
        /// Удалить продукт
        /// </summary>
        /// <param name="id">Идентификатор продукта</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productRepo.Remove(id);
            return Ok();
        }
    }
}
