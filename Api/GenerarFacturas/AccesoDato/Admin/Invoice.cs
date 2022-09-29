using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerAccess.Admin
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string ClientName { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string ClientId { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string ClienteAddress { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string ClientePhone { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string InvoiceNumber { get; set; }


        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }

        [Column(TypeName ="datetime2")]
        public DateTime Created { get; set; } = DateTime.Now;

        public Company Company { get; set; }

        [Column(TypeName = "varchar(1)")]
        public string Status { get; set; }

        public List<InvoiceDetail> Items { get; set; }
    }
}
