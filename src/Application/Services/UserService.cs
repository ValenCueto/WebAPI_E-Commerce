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

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User? GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public int Create(UserToCreate userToCreate)
        {
            var user = new User()
            {
                Name = userToCreate.Name,
                Email = userToCreate.Email,
                Password = userToCreate.Password,
                Rol = RolEnum.Client
            };


            _userRepository.Create(user);
            return user.Id;
        }

        public void Delete(int id)
        {
            var user = _userRepository.GetById(id);
            _userRepository.Delete(user);
        }

        public void Update(UserToUpdate userToUpdate, int id)
        {
            var user = _userRepository.GetById(id);
            user.Name = userToUpdate.Name;
            user.Email = userToUpdate.Email;
            user.Password = userToUpdate.Password;  
            _userRepository.Update(user);
        }
    }


}
