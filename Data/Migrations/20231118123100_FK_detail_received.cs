using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoBiaNGK.Data.Migrations
{
    public partial class FK_detail_received : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailReceiveds_ReceivedBills_ReceivedBillId",
                table: "DetailReceiveds");

            migrationBuilder.DropIndex(
                name: "IX_DetailReceiveds_ReceivedBillId",
                table: "DetailReceiveds");

            migrationBuilder.DropColumn(
                name: "ReceivedBillId",
                table: "DetailReceiveds");

            migrationBuilder.CreateIndex(
                name: "IX_DetailReceiveds_BillId",
                table: "DetailReceiveds",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailReceiveds_ReceivedBills_BillId",
                table: "DetailReceiveds",
                column: "BillId",
                principalTable: "ReceivedBills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailReceiveds_ReceivedBills_BillId",
                table: "DetailReceiveds");

            migrationBuilder.DropIndex(
                name: "IX_DetailReceiveds_BillId",
                table: "DetailReceiveds");

            migrationBuilder.AddColumn<int>(
                name: "ReceivedBillId",
                table: "DetailReceiveds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetailReceiveds_ReceivedBillId",
                table: "DetailReceiveds",
                column: "ReceivedBillId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailReceiveds_ReceivedBills_ReceivedBillId",
                table: "DetailReceiveds",
                column: "ReceivedBillId",
                principalTable: "ReceivedBills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
