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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EInvoiceLine>()
            .HasOne(c => c.Item).WithMany(c => c.EInvoiceLines)
            .HasForeignKey(c => c.ItemId)
            .OnDelete(DeleteBehavior.ClientNoAction);

        modelBuilder.Entity<EInvoice>()
            .HasOne(c => c.Customer).WithMany(c => c.EInvoices)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.ClientNoAction);

        modelBuilder.Entity<EInvoiceLineTax>()
            .HasOne(c => c.Tax).WithMany(c => c.EInvoiceLineTaxes)
            .HasForeignKey(c => c.TaxId)
            .OnDelete(DeleteBehavior.ClientNoAction);

        //The following code will set ON DELETE NO ACTION to all Foreign Keys
        //foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        //    foreignKey.DeleteBehavior = DeleteBehavior.ClientNoAction;

        base.OnModelCreating(modelBuilder);
    }

    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
    {
        if (entity is Entity model)
        {
            model.CreationDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
        }
        return base.Add(entity);
    }

    public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
    {
        if (entity is Entity model)
            model.ModifiedDate = DateTime.Now;
        return base.Update(entity);
    }
}
