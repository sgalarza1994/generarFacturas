using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerAccess.Admin
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Column(TypeName ="varchar(200)")]
        public string BusinessName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Phone { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public User User { get; set; }

        public List<Invoice> Invoices { get; set; }
    }
}
