using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
        User? GetById(int id);
        int Create(UserToCreate userToCreate);
        void Delete(int id);
        void Update(UserToUpdate userToUpdate, int id);
    }
}
