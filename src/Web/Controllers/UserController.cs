
using Application.Interfaces;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[Action]")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("[Action]/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok(_userService.GetById(id));
        }

        [HttpGet("[Action]/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            return Ok(_userService.GetByName(name));
        }

        [HttpPost("[Action]")]
        public IActionResult Create(UserToCreate userToCreate)
        {
             _userService.Create(userToCreate);
            return Ok(userToCreate.Name);
        }

        [HttpDelete("[Action]/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        [HttpPut("[Action]/{id}")]
        public IActionResult Update(UserToUpdate userToUpdate,[FromRoute] int id)
        {
            try
            {
                _userService.Update(userToUpdate, id);
                return Ok();

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }
    }
}
