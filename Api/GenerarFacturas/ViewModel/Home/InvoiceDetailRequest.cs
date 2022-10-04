using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Home
{
    public class InvoiceDetailRequest
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        public decimal UnitPrice { get; set; }

        public int Tax { get; set; }
    }
}
