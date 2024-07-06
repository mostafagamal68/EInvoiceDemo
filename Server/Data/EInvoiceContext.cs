using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EInvoiceDemo.Server.Data;

public class EInvoiceContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<EInvoice> EInvoices { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<EInvoiceLine> EInvoiceLines { get; set; }
    public DbSet<Tax> Taxes { get; set; }
    public DbSet<EInvoiceLineTax> EInvoiceLineTaxes { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
    {
        (entity as Entity)!.CreationDate = DateTime.Now;
        return base.Add(entity);
    }
    public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
    {
        (entity as Entity)!.ModifiedDate = DateTime.Now;
        return base.Update(entity);
    }
}
