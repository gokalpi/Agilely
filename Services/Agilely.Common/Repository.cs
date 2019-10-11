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

        public bool Delete(object id)
        {
            Check.NotNull(id, nameof(id));

            T entity = GetById(id);
            if (entity == null)
                throw new Exception($"No entity found with id {id}");

            return Delete(entity);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            Check.NotNull(id, nameof(id));

            T entity = await GetByIdAsync(id);
            if (entity == null)
                throw new Exception($"No entity found with id {id}");

            return (await DeleteAsync(entity));
        }

        public bool Delete(T entity)
        {
            Check.NotNull(entity, nameof(entity));

            var dbSet = _context.Set<T>();
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
            return _context.SaveChanges() == 1;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            Check.NotNull(entity, nameof(entity));

            var dbSet = _context.Set<T>();
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
            return (await _context.SaveChangesAsync()) == 1;
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