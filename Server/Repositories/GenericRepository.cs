using EInvoiceDemo.Server.Data;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceDemo.Server.Repositories;

public class GenericRepository<TEntity>(EInvoiceContext context) : IGenericRepository<TEntity> where TEntity : Entity
{
    public void Add(TEntity model)
    {
        context.Add(model);
        DbSet.Add(model);
    }

    public DbSet<TEntity> DbSet => context.Set<TEntity>();

    public void Delete(TEntity model) => DbSet.Remove(model);

    public bool Exists(Guid id) => DbSet.Any(e => e.Id == id);
    
    public async Task<TEntity> GetAsync(Guid id) => await Query().FirstOrErrorAsync(id);

    public virtual IQueryable<TEntity> Query() => DbSet.AsQueryable();

    public void Update(TEntity model) => context.Update(model);

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
}
