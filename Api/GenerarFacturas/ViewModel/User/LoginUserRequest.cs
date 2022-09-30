using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Validator;

namespace ViewModel.User
{
    public class LoginUserRequest
    {
        [Validator(true, 5)]
        public string Email { get; set; }
        [Validator(true, 8)]
        public string Password { get; set; }
    }
}
