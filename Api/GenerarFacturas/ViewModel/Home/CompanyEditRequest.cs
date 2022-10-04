using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Home
{
    public class CompanyEditRequest
    {
        public int CompanyId { get; set; }
        public string BusinessName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
