using System.Linq.Expressions;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetByIdAsync(string id);

    Task<TEntity> AddAsync(TEntity entity);
    Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);

    Task<TEntity> UpdateAsync(string id, TEntity entity);
    Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity);

    Task<TEntity> DeleteAsync(TEntity entity);
    Task<TEntity> DeleteAsync(string id);
    Task<TEntity> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
}