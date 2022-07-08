using System.Linq.Expressions;
using Inventory.Application.Interfaces.Repositories;
using Inventory.Data.Context;
using Inventory.Domain.Entities;
using MongoDB.Driver;

namespace Inventory.Data.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IMongoCollection<TEntity> _dbCollection;

    public GenericRepository(IMongoDbContext context)
    {
        _dbCollection = context.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
    {
        return predicate is null ? _dbCollection.AsQueryable() : _dbCollection.AsQueryable().Where(predicate);
    }

    public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbCollection.Find(predicate).FirstOrDefaultAsync();
    }

    public virtual Task<TEntity> GetByIdAsync(string id)
    {
        return _dbCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbCollection.InsertOneAsync(entity);
        return entity;
    }

    public virtual async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        return (await _dbCollection.BulkWriteAsync((IEnumerable<WriteModel<TEntity>>)entities)).IsAcknowledged;
    }

    public virtual async Task<TEntity> UpdateAsync(string id, TEntity entity)
    {
        return await _dbCollection.FindOneAndReplaceAsync(x => x.Id == id, entity);
    }

    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity)
    {
        return await _dbCollection.FindOneAndReplaceAsync(predicate, entity);
    }

    public virtual async Task<TEntity> DeleteAsync(TEntity entity)
    {
        return await _dbCollection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
    }

    public virtual async Task<TEntity> DeleteAsync(string id)
    {
        return await _dbCollection.FindOneAndDeleteAsync(x => x.Id == id);
    }

    public virtual async Task<TEntity> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbCollection.FindOneAndDeleteAsync(predicate);
    }
}