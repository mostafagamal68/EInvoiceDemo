﻿// <auto-generated />
using System;
using EInvoiceDemo.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EInvoiceDemo.Server.Migrations
{
    [DbContext(typeof(EInvoiceContext))]
    partial class EInvoiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EInvoiceDemo.Server.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.EInvoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTimeIssued")
                        .HasColumnType("datetime2");

                    b.Property<int>("EInvoiceType")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("decimal(28, 8)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("EInvoices");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.EInvoiceLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AmountSold")
                        .HasColumnType("decimal(28, 8)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EInvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ItemNetAmount")
                        .HasColumnType("decimal(28, 8)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(28, 8)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(28, 8)");

                    b.HasKey("Id");

                    b.HasIndex("EInvoiceId");

                    b.HasIndex("ItemId");

                    b.ToTable("EInvoiceLines");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.EInvoiceLineTax", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(28, 8)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EInvoiceLineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TaxId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EInvoiceLineId");

                    b.HasIndex("TaxId");

                    b.ToTable("EInvoiceLineTaxes");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ItemDescription")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.Tax", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TaxDescription")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("TaxName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Taxes");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.EInvoice", b =>
                {
                    b.HasOne("EInvoiceDemo.Server.Models.Customer", "Customer")
                        .WithMany("EInvoices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.EInvoiceLine", b =>
                {
                    b.HasOne("EInvoiceDemo.Server.Models.EInvoice", "EInvoice")
                        .WithMany("EInvoiceLines")
                        .HasForeignKey("EInvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EInvoiceDemo.Server.Models.Item", "Item")
                        .WithMany("EInvoiceLines")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("EInvoice");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.EInvoiceLineTax", b =>
                {
                    b.HasOne("EInvoiceDemo.Server.Models.EInvoiceLine", "EInvoiceLine")
                        .WithMany("EInvoiceLineTaxes")
                        .HasForeignKey("EInvoiceLineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EInvoiceDemo.Server.Models.Tax", "Tax")
                        .WithMany("EInvoiceLineTaxes")
                        .HasForeignKey("TaxId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("EInvoiceLine");

                    b.Navigation("Tax");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.Customer", b =>
                {
                    b.Navigation("EInvoices");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.EInvoice", b =>
                {
                    b.Navigation("EInvoiceLines");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.EInvoiceLine", b =>
                {
                    b.Navigation("EInvoiceLineTaxes");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.Item", b =>
                {
                    b.Navigation("EInvoiceLines");
                });

            modelBuilder.Entity("EInvoiceDemo.Server.Models.Tax", b =>
                {
                    b.Navigation("EInvoiceLineTaxes");
                });
#pragma warning restore 612, 618
        }
    }
}
