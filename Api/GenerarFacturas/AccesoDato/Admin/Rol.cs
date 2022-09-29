using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerAccess.Admin
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; }
        [Column(TypeName = "varchar(1)")]
        public string Status { get; set; }

        public List<User> Users { get; set; }
    }
}
