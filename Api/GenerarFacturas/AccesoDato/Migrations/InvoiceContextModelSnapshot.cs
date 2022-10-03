﻿// <auto-generated />
using System;
using LayerAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LayerAccess.Migrations
{
    [DbContext(typeof(InvoiceContext))]
    partial class InvoiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LayerAccess.Admin.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("BusinessName")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("LayerAccess.Admin.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientId")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("ClientName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ClienteAddress")
                        .HasColumnType("varchar(300)");

                    b.Property<string>("ClientePhone")
                        .HasColumnType("varchar(15)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(1)");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("LayerAccess.Admin.InvoiceDetail", b =>
                {
                    b.Property<int>("InvoiceDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(200)");

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("Tax")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceDetailId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceDetails");
                });

            modelBuilder.Entity("LayerAccess.Admin.Rol", b =>
                {
                    b.Property<int>("RolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(1)");

                    b.HasKey("RolId");

                    b.ToTable("Rols");

                    b.HasData(
                        new
                        {
                            RolId = 1,
                            Description = "ADMINISTRADOR",
                            Status = "A"
                        },
                        new
                        {
                            RolId = 2,
                            Description = "USUARIO",
                            Status = "A"
                        });
                });

            modelBuilder.Entity("LayerAccess.Admin.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(2)");

                    b.Property<byte[]>("Valor")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RolId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LayerAccess.Admin.Company", b =>
                {
                    b.HasOne("LayerAccess.Admin.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LayerAccess.Admin.Invoice", b =>
                {
                    b.HasOne("LayerAccess.Admin.Company", "Company")
                        .WithMany("Invoices")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("LayerAccess.Admin.InvoiceDetail", b =>
                {
                    b.HasOne("LayerAccess.Admin.Invoice", "Invoice")
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("LayerAccess.Admin.User", b =>
                {
                    b.HasOne("LayerAccess.Admin.Rol", "Rol")
                        .WithMany("Users")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("LayerAccess.Admin.Company", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("LayerAccess.Admin.Invoice", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LayerAccess.Admin.Rol", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
