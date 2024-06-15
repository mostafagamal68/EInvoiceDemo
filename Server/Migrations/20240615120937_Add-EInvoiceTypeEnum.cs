using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EInvoiceDemo.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddEInvoiceTypeEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EInvoices_EInvoiceTypes_EInvoiceTypeId",
                table: "EInvoices");

            migrationBuilder.DropTable(
                name: "EInvoiceTypes");

            migrationBuilder.DropIndex(
                name: "IX_EInvoices_EInvoiceTypeId",
                table: "EInvoices");

            migrationBuilder.DropColumn(
                name: "EInvoiceTypeId",
                table: "EInvoices");

            migrationBuilder.AddColumn<int>(
                name: "EInvoiceType",
                table: "EInvoices",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EInvoiceType",
                table: "EInvoices");

            migrationBuilder.AddColumn<Guid>(
                name: "EInvoiceTypeId",
                table: "EInvoices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "EInvoiceTypes",
                columns: table => new
                {
                    EInvoiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EInvoiceTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EInvoiceTypes", x => x.EInvoiceTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EInvoices_EInvoiceTypeId",
                table: "EInvoices",
                column: "EInvoiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EInvoices_EInvoiceTypes_EInvoiceTypeId",
                table: "EInvoices",
                column: "EInvoiceTypeId",
                principalTable: "EInvoiceTypes",
                principalColumn: "EInvoiceTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
