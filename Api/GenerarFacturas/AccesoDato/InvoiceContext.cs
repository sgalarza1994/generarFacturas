using LayerAccess.Admin;
using Microsoft.EntityFrameworkCore;
namespace LayerAccess
{
    public class InvoiceContext :DbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options)
            :base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Rols { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().HasData(
                new Rol { RolId = 1,Description  ="ADMINISTRADOR",Status  ="A"},
                new Rol { RolId = 2,Description = "USUARIO",Status  ="A"}
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
