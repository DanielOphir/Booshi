using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooshiDAL.Migrations
{
    public partial class Addeddeliveryoriginanddestination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HouseNumber",
                table: "UsersDetails",
                newName: "ZipCode");

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.DeliveryId);
                    table.ForeignKey(
                        name: "FK_Destinations_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Origins",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Origins", x => x.DeliveryId);
                    table.ForeignKey(
                        name: "FK_Origins_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "Origins");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "UsersDetails",
                newName: "HouseNumber");

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryManId",
                table: "Deliveries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DeliveryManId",
                table: "Deliveries",
                column: "DeliveryManId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Users_DeliveryManId",
                table: "Deliveries",
                column: "DeliveryManId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
