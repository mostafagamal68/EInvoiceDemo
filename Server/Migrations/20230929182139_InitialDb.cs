using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EInvoiceDemo.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "EInvoiceTypes",
                columns: table => new
                {
                    EInvoiceTypeId = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    EInvoiceTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EInvoiceTypes", x => x.EInvoiceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ItemCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    TaxId = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    TaxName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TaxDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TaxCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.TaxId);
                });

            migrationBuilder.CreateTable(
                name: "EInvoices",
                columns: table => new
                {
                    EInvoiceId = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    EInvoiceCode = table.Column<int>(type: "int", nullable: false),
                    EInvoiceTypeId = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    CustomerId = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    DateTimeIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(28,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EInvoices", x => x.EInvoiceId);
                    table.ForeignKey(
                        name: "FK_EInvoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EInvoices_EInvoiceTypes_EInvoiceTypeId",
                        column: x => x.EInvoiceTypeId,
                        principalTable: "EInvoiceTypes",
                        principalColumn: "EInvoiceTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EInvoiceLines",
                columns: table => new
                {
                    EInvoiceLineId = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    ItemId = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    EInvoiceId = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    AmountSold = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    ItemNetAmount = table.Column<decimal>(type: "decimal(28,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EInvoiceLines", x => x.EInvoiceLineId);
                    table.ForeignKey(
                        name: "FK_EInvoiceLines_EInvoices_EInvoiceId",
                        column: x => x.EInvoiceId,
                        principalTable: "EInvoices",
                        principalColumn: "EInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EInvoiceLines_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EInvoiceLineTaxes",
                columns: table => new
                {
                    EInvoiceLineTaxId = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxId = table.Column<decimal>(type: "decimal(28,8)", nullable: false),
                    EInvoiceLineId = table.Column<decimal>(type: "decimal(28,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EInvoiceLineTaxes", x => x.EInvoiceLineTaxId);
                    table.ForeignKey(
                        name: "FK_EInvoiceLineTaxes_EInvoiceLines_EInvoiceLineId",
                        column: x => x.EInvoiceLineId,
                        principalTable: "EInvoiceLines",
                        principalColumn: "EInvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EInvoiceLineTaxes_Taxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Taxes",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EInvoiceLines_EInvoiceId",
                table: "EInvoiceLines",
                column: "EInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_EInvoiceLines_ItemId",
                table: "EInvoiceLines",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EInvoiceLineTaxes_EInvoiceLineId",
                table: "EInvoiceLineTaxes",
                column: "EInvoiceLineId");

            migrationBuilder.CreateIndex(
                name: "IX_EInvoiceLineTaxes_TaxId",
                table: "EInvoiceLineTaxes",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_EInvoices_CustomerId",
                table: "EInvoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_EInvoices_EInvoiceTypeId",
                table: "EInvoices",
                column: "EInvoiceTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EInvoiceLineTaxes");

            migrationBuilder.DropTable(
                name: "EInvoiceLines");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "EInvoices");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "EInvoiceTypes");
        }
    }
}
