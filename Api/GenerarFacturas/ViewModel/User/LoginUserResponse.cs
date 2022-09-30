using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.User
{
    public class LoginUserResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public int RolId { get; set; }
        public string RolName { get; set; }

        public int CompanyId { get; set; }
    }
}
