using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("[Action]")]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet("[Action]/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok(_productService.GetById(id));
        }

        [HttpPost("[Action]")]
        public IActionResult Create([FromBody]ProductToCreate productToCreate)
        {
            return Ok(_productService.Create(productToCreate));
        }

        [HttpDelete("[Action]/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _productService.Delete(id);
            return Ok();
        }

        [HttpPut("[Action]/{id}")]
        public IActionResult Update([FromBody]ProductToUpdate productToUpdate, [FromRoute] int id)
        {
            try
            {
                _productService.Update(productToUpdate, id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("[Action]/ {name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            return Ok(_productService.GetByName(name));
        }
    }


}
