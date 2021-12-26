using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooshiDAL.Migrations
{
    public partial class Addeddeliveryperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryPersonId",
                table: "Deliveries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeliveryPeople",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPeople", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_DeliveryPeople_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DeliveryPersonId",
                table: "Deliveries",
                column: "DeliveryPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_DeliveryPeople_DeliveryPersonId",
                table: "Deliveries",
                column: "DeliveryPersonId",
                principalTable: "DeliveryPeople",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_DeliveryPeople_DeliveryPersonId",
                table: "Deliveries");

            migrationBuilder.DropTable(
                name: "DeliveryPeople");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_DeliveryPersonId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "DeliveryPersonId",
                table: "Deliveries");
        }
    }
}
