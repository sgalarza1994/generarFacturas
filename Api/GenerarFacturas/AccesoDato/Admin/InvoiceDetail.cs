using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerAccess.Admin
{
    public class InvoiceDetail
    {
        [Key]
        public int InvoiceDetailId { get; set; }

        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; }
        [Column(TypeName ="varchar(200)")]
        public string Description { get; set; }
        public int Amount { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        public int Tax { get; set; }
    }
}
