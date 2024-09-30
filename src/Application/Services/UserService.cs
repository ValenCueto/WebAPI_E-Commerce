using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int Create(UserDto userDto)
        {
            var user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                Rol = RolEnum.Client
            };


            _userRepository.Create(user);
            return user.Id;
        }

        public void Delete(int Id)
        {

        }
    }


}
