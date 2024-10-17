using Application.Interfaces;
using Application.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        private bool IsSeller()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == RolEnum.Seller.ToString())
            {
                return true;
            }
            return false;
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

        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult Create([FromBody]ProductToCreate productToCreate)
        {
            if (!IsSeller())
            {
                return Forbid();
            }
            return Ok(_productService.Create(productToCreate));
        }

        [Authorize]
        [HttpDelete("[Action]/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!IsSeller())
            {
                return Forbid();
            }
            _productService.Delete(id);
            return Ok();
        }

        [Authorize]
        [HttpPut("[Action]/{id}")]
        public IActionResult Update([FromBody]ProductToUpdate productToUpdate, [FromRoute] int id)
        {
            if (!IsSeller())
            {
                return Forbid();
            }
           
            _productService.Update(productToUpdate, id);
            return Ok();
        }

        [HttpGet("[Action]/ {name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            return Ok(_productService.GetByName(name));
        }
    }


}
