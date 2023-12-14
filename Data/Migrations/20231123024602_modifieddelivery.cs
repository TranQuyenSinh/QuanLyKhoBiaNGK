using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoBiaNGK.Data.Migrations
{
    public partial class modifieddelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBillItem_DeliveryBill_DeliveryBillId",
                table: "DeliveryBillItem");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBillItem_Products_ProductId",
                table: "DeliveryBillItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryBillItem",
                table: "DeliveryBillItem");

            migrationBuilder.RenameTable(
                name: "DeliveryBillItem",
                newName: "DeliveryBillItems");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryBillItem_ProductId",
                table: "DeliveryBillItems",
                newName: "IX_DeliveryBillItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryBillItem_DeliveryBillId",
                table: "DeliveryBillItems",
                newName: "IX_DeliveryBillItems_DeliveryBillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryBillItems",
                table: "DeliveryBillItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBillItems_DeliveryBill_DeliveryBillId",
                table: "DeliveryBillItems",
                column: "DeliveryBillId",
                principalTable: "DeliveryBill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBillItems_Products_ProductId",
                table: "DeliveryBillItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBillItems_DeliveryBill_DeliveryBillId",
                table: "DeliveryBillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryBillItems_Products_ProductId",
                table: "DeliveryBillItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryBillItems",
                table: "DeliveryBillItems");

            migrationBuilder.RenameTable(
                name: "DeliveryBillItems",
                newName: "DeliveryBillItem");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryBillItems_ProductId",
                table: "DeliveryBillItem",
                newName: "IX_DeliveryBillItem_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryBillItems_DeliveryBillId",
                table: "DeliveryBillItem",
                newName: "IX_DeliveryBillItem_DeliveryBillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryBillItem",
                table: "DeliveryBillItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBillItem_DeliveryBill_DeliveryBillId",
                table: "DeliveryBillItem",
                column: "DeliveryBillId",
                principalTable: "DeliveryBill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryBillItem_Products_ProductId",
                table: "DeliveryBillItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
