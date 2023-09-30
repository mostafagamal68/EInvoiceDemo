using EInvoiceDemo.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceDemo.Server.Data;

public class EInvoiceContext : DbContext
{
    public EInvoiceContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<EInvoiceType> EInvoiceTypes { get; set; }
    public DbSet<EInvoice> EInvoices { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<EInvoiceLine> EInvoiceLines { get; set; }
    public DbSet<Tax> Taxes { get; set; }
    public DbSet<EInvoiceLineTax> EInvoiceLineTaxes { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
