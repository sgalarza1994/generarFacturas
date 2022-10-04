using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Validator;

namespace ViewModel.Home
{
    public class InvoiceRequest
    {
        [Validator(true, 5)]
        public string ClientName { get; set; }

        [Validator(true, 10)]
        public string ClientId { get; set; }

        
        public string ClientAddress { get; set; }

        public string ClientPhone { get; set; }

        public string InvoiceNumber { get; set; }

        public int CompanyId { get; set; }
    }
}
