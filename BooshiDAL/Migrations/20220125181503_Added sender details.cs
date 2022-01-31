using Microsoft.EntityFrameworkCore.Migrations;

namespace BooshiDAL.Migrations
{
    public partial class Addedsenderdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Origins",
                type: "nvarchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Origins",
                type: "nvarchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Origins",
                type: "nvarchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Origins",
                type: "nvarchar(256)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Origins");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Origins");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Origins");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Origins");
        }
    }
}
