using Microsoft.EntityFrameworkCore.Migrations;

namespace LayerAccess.Migrations
{
    public partial class companyRuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ruc",
                table: "Companies",
                type: "varchar(20)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ruc",
                table: "Companies");
        }
    }
}
