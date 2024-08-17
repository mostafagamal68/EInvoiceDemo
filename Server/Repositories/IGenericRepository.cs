using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceDemo.Server.Repositories;

public interface IGenericRepository<TEntity> where TEntity : Entity
{
    DbSet<TEntity> DbSet { get; }
    IQueryable<TEntity> Query();
    Task<TEntity> GetAsync(Guid id);
    void Update(TEntity model);
    void Add(TEntity model);
    void Delete(TEntity id);
    bool Exists(Guid id);
    Task<int> SaveChangesAsync();
}
