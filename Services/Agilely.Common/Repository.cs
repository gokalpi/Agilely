using Agilely.Common.Contracts;
using Agilely.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Agilely.Common
{
    public class Repository<TContext, T> : ReadOnlyRepository<TContext, T>, IRepository<T>
        where TContext : DbContext
        where T : class
    {
        public Repository(TContext context) : base(context)
        {
        }

        public T Create(T entity)
        {
            Check.NotNull(entity, nameof(entity));

            T createdEntity = _context.Set<T>().Add(entity).Entity;

            _context.SaveChanges();

            return createdEntity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            Check.NotNull(entity, nameof(entity));

            T createdEntity = (await _context.Set<T>().AddAsync(entity)).Entity;

            await _context.SaveChangesAsync();

            return createdEntity;
        }

        public void Delete(Expression<Func<T, bool>> filter)
        {
            Check.NotNull(filter, nameof(filter));

            var entities = Get(filter);
            if (entities == null)
                throw new Exception("No entities found for deletion.");

            _context.Set<T>().AttachRange(entities);
            _context.Set<T>().RemoveRange(entities);

            _context.SaveChanges();
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> filter)
        {
            Check.NotNull(filter, nameof(filter));

            var entities = await GetAsync(filter);
            if (entities == null)
                throw new Exception("No entities found for deletion.");

            _context.Set<T>().AttachRange(entities);
            _context.Set<T>().RemoveRange(entities);

            await _context.SaveChangesAsync();
        }

        public void Delete(object id)
        {
            Check.NotNull(id, nameof(id));

            T entity = GetById(id);
            if (entity == null)
                throw new Exception($"No entity found with id {id}");

            Delete(entity);
        }

        public async Task DeleteAsync(object id)
        {
            Check.NotNull(id, nameof(id));

            T entity = await GetByIdAsync(id);
            if (entity == null)
                throw new Exception($"No entity found with id {id}");

            await DeleteAsync(entity);
        }

        public void Delete(T entity)
        {
            Check.NotNull(entity, nameof(entity));

            var dbSet = _context.Set<T>();
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            Check.NotNull(entity, nameof(entity));

            var dbSet = _context.Set<T>();
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public T Update(T entity)
        {
            Check.NotNull(entity, nameof(entity));

            var entry = _context.Entry(entity);
            if (entry.State != EntityState.Detached)
                _context.Set<T>().Attach(entity);

            entry.State = EntityState.Modified;

            _context.SaveChanges();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            Check.NotNull(entity, nameof(entity));

            var entry = _context.Entry(entity);
            if (entry.State != EntityState.Detached)
                _context.Set<T>().Attach(entity);

            entry.State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return await Task.FromResult(entity);
        }
    }
}