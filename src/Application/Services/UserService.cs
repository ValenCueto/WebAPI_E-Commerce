using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService
    {
        private readonly UserService _userService;

        public UserService(UserService userService)
        {
            _userService = userService;
        }
    }
}
