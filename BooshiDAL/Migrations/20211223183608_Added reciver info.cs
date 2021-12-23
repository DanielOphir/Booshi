using Microsoft.EntityFrameworkCore.Migrations;

namespace BooshiDAL.Migrations
{
    public partial class Addedreciverinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReciverFirstName",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReciverLastName",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevicerEmail",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevicerPhoneNumber",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReciverFirstName",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "ReciverLastName",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "RevicerEmail",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "RevicerPhoneNumber",
                table: "Destinations");
        }
    }
}
