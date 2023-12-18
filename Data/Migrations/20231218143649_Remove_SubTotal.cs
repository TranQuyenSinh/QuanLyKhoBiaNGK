using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoBiaNGK.Data.Migrations
{
    public partial class Remove_SubTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "DeliveryBill");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubTotal",
                table: "DeliveryBill",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
