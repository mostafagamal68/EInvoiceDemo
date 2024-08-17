using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EInvoiceDemo.Server.Migrations
{
    /// <inheritdoc />
    public partial class NoActionOnDeleteForSetupData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EInvoiceLines_Items_ItemId",
                table: "EInvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_EInvoiceLineTaxes_Taxes_TaxId",
                table: "EInvoiceLineTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_EInvoices_Customers_CustomerId",
                table: "EInvoices");

            migrationBuilder.AddForeignKey(
                name: "FK_EInvoiceLines_Items_ItemId",
                table: "EInvoiceLines",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EInvoiceLineTaxes_Taxes_TaxId",
                table: "EInvoiceLineTaxes",
                column: "TaxId",
                principalTable: "Taxes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EInvoices_Customers_CustomerId",
                table: "EInvoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EInvoiceLines_Items_ItemId",
                table: "EInvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_EInvoiceLineTaxes_Taxes_TaxId",
                table: "EInvoiceLineTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_EInvoices_Customers_CustomerId",
                table: "EInvoices");

            migrationBuilder.AddForeignKey(
                name: "FK_EInvoiceLines_Items_ItemId",
                table: "EInvoiceLines",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EInvoiceLineTaxes_Taxes_TaxId",
                table: "EInvoiceLineTaxes",
                column: "TaxId",
                principalTable: "Taxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EInvoices_Customers_CustomerId",
                table: "EInvoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
