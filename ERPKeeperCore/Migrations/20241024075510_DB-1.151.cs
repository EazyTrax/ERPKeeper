using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB1151 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReceivePayments_SaleId",
                table: "ReceivePayments");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayments_SaleId",
                table: "ReceivePayments",
                column: "SaleId",
                unique: true,
                filter: "[SaleId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReceivePayments_SaleId",
                table: "ReceivePayments");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayments_SaleId",
                table: "ReceivePayments",
                column: "SaleId");
        }
    }
}
