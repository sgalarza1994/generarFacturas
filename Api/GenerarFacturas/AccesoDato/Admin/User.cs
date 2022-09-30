using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerAccess.Admin
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Password { get; set; }
        public byte[] Valor { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string LastName { get; set; }


        [Column(TypeName = "varchar(2)")]
        public string Status { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; } = DateTime.Now;

        [ForeignKey("RolId")]
        public int RolId { get; set; }

        public Rol Rol { get; set; }

       
    }
}
