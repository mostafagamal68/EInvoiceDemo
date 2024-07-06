using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EInvoiceDemo.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxCode",
                table: "Taxes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "TaxId",
                table: "Taxes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ItemCode",
                table: "Items",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Items",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EInvoiceCode",
                table: "EInvoices",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "EInvoiceId",
                table: "EInvoices",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EInvoiceLineTaxId",
                table: "EInvoiceLineTaxes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EInvoiceLineId",
                table: "EInvoiceLines",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CustomerCode",
                table: "Customers",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customers",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Taxes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Taxes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "EInvoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "EInvoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "EInvoiceLineTaxes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "EInvoiceLineTaxes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "EInvoiceLines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "EInvoiceLines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "EInvoices");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "EInvoices");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "EInvoiceLineTaxes");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "EInvoiceLineTaxes");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "EInvoiceLines");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "EInvoiceLines");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Taxes",
                newName: "TaxCode");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Taxes",
                newName: "TaxId");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Items",
                newName: "ItemCode");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Items",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "EInvoices",
                newName: "EInvoiceCode");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EInvoices",
                newName: "EInvoiceId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EInvoiceLineTaxes",
                newName: "EInvoiceLineTaxId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EInvoiceLines",
                newName: "EInvoiceLineId");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Customers",
                newName: "CustomerCode");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "CustomerId");
        }
    }
}
