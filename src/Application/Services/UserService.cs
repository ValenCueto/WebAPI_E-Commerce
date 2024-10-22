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

        public List<UserToResponse> GetAll()
        {
            var users = _userRepository.GetAll();

            var userToResponseList = new List<UserToResponse>();

            foreach (var user in users)
            {
                var userToResponse = new UserToResponse()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                };
                userToResponseList.Add(userToResponse);
            }
            return userToResponseList;
        }

        public UserToResponse? GetById(int id)
        {
            var user = _userRepository.GetById(id);
            var userToResponse = new UserToResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
            return userToResponse;
        }

        public int Create(UserToCreate userToCreate)
        {
            var existingUserName = _userRepository.GetByName(userToCreate.Name);
            var existingUserEmail = _userRepository.GetByEmail(userToCreate.Email);
            if (existingUserName is not null || existingUserEmail is not null)
            {
                throw new Exception("Ya existe un usuario con ese nombre o email");
            }
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
            if (user == null)
            {
                throw new Exception($"El usuario con ID {id} no fue encontrado.");
            }
            user.Name = userToUpdate.Name;
            user.Email = userToUpdate.Email;
            user.Password = userToUpdate.Password;  
            _userRepository.Update(user);
        }

        public UserToResponse? GetByName(string name)
        {
            var user = _userRepository.GetByName(name);
            var userToResponse = new UserToResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
            return userToResponse;
        }
    }


}
