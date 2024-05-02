using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPKeeperCore.Enterprise.Migrations
{
    /// <inheritdoc />
    public partial class DB133 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayment_Customers_CustomerId",
                table: "ReceivePayment");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayment_Transactions_TransactionId",
                table: "ReceivePayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceivePayment",
                table: "ReceivePayment");

            migrationBuilder.RenameTable(
                name: "ReceivePayment",
                newName: "ReceivePayments");

            migrationBuilder.RenameIndex(
                name: "IX_ReceivePayment_TransactionId",
                table: "ReceivePayments",
                newName: "IX_ReceivePayments_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceivePayment_CustomerId",
                table: "ReceivePayments",
                newName: "IX_ReceivePayments_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceivePayments",
                table: "ReceivePayments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SupplierPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    No = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierPayments_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupplierPayments_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_SupplierId",
                table: "SupplierPayments",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_TransactionId",
                table: "SupplierPayments",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_Customers_CustomerId",
                table: "ReceivePayments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayments_Transactions_TransactionId",
                table: "ReceivePayments",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_Customers_CustomerId",
                table: "ReceivePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayments_Transactions_TransactionId",
                table: "ReceivePayments");

            migrationBuilder.DropTable(
                name: "SupplierPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceivePayments",
                table: "ReceivePayments");

            migrationBuilder.RenameTable(
                name: "ReceivePayments",
                newName: "ReceivePayment");

            migrationBuilder.RenameIndex(
                name: "IX_ReceivePayments_TransactionId",
                table: "ReceivePayment",
                newName: "IX_ReceivePayment_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceivePayments_CustomerId",
                table: "ReceivePayment",
                newName: "IX_ReceivePayment_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceivePayment",
                table: "ReceivePayment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayment_Customers_CustomerId",
                table: "ReceivePayment",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayment_Transactions_TransactionId",
                table: "ReceivePayment",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}
