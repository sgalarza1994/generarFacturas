using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Validator;

namespace ViewModel.Home
{
    public class InvoiceResponse
    {
      public int InvoiceId { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }  
        public string ClientAddress { get; set; }

        public string ClientPhone { get; set; }

        public string InvoiceNumber { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public decimal Total { get; set; }
    }
}
