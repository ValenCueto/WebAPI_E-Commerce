
using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        private bool IsSeller()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == RolEnum.Seller.ToString())
            {
                return true;
            }
            return false;
        }

        private bool IsClient()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == RolEnum.Client.ToString())
            {
                return true;
            }
            return false;
        }
        private int GetAuthenticatedUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : -1;
        }

        [Authorize]
        [HttpGet("[Action]")]
        public IActionResult GetAll()
        {
            if (!IsSeller())
            {
                return Forbid();
            }
            return Ok(_userService.GetAll());
        }

        [Authorize]
        [HttpGet("[Action]/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            if (!IsSeller())
            {
                return Forbid();
            }
            return Ok(_userService.GetById(id));
        }

        [Authorize]
        [HttpGet("[Action]/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            if (!IsSeller())
            {
                return Forbid();
            }
            return Ok(_userService.GetByName(name));
        }

        [HttpPost("[Action]")]
        public IActionResult Create([FromBody] UserToCreate userToCreate)
        {
             _userService.Create(userToCreate);
            return Ok(userToCreate.Name);
        }


        [Authorize]
        [HttpPut("[Action]/{id}")]
        public IActionResult Update([FromBody] UserToUpdate userToUpdate,[FromRoute] int id)
        {
            if (!IsClient())
            {
                return Forbid();
            }

            var userAuthenticated = GetAuthenticatedUserId();
            if (userAuthenticated == id)
            {
                _userService.Update(userToUpdate, id);
                return Ok();
            }
            return BadRequest();

            
        }
    }
}
