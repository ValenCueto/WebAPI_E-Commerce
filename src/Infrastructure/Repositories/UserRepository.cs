using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context) 
        {

        }

        public User? GetByName(string name)
        {
            return _context.Users.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
        }
    }
}
