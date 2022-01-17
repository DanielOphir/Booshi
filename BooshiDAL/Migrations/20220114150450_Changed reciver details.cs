using Microsoft.EntityFrameworkCore.Migrations;

namespace BooshiDAL.Migrations
{
    public partial class Changedreciverdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RevicerPhoneNumber",
                table: "Destinations",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "RevicerEmail",
                table: "Destinations",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "ReciverLastName",
                table: "Destinations",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "ReciverFirstName",
                table: "Destinations",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Destinations",
                newName: "RevicerPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Destinations",
                newName: "RevicerEmail");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Destinations",
                newName: "ReciverLastName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Destinations",
                newName: "ReciverFirstName");
        }
    }
}
