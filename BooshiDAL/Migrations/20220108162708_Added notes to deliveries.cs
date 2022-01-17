using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooshiDAL.Migrations
{
    public partial class Addednotestodeliveries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_DeliveryPeople_DeliveryPersonId",
                table: "Deliveries");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeliveryPersonId",
                table: "Deliveries",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Deliveries");

            migrationBuilder.RenameColumn(
                name: "IsActiveUser",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsActiveDeliveryPerson",
                table: "DeliveryPeople",
                newName: "IsActive");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeliveryPersonId",
                table: "Deliveries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_DeliveryPeople_DeliveryPersonId",
                table: "Deliveries",
                column: "DeliveryPersonId",
                principalTable: "DeliveryPeople",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
