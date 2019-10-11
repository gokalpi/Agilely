using Agilely.Common.Contracts;
using Agilely.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Agilely.Common
{
    public class ReadOnlyRepository<TContext, T> : IReadOnlyRepository<T>
        where TContext : DbContext
        where T : class
                                                                         
    {
        protected readonly TContext _context;

        public ReadOnlyRepository(TContext context)
        {
            _context = Check.NotNull(context, nameof(context));
        }

        public int Count(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            return await GetQueryable(filter).CountAsync();
        }

        public bool Exists(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            return await GetQueryable(filter).AnyAsync();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            Check.NotNull(filter, nameof(filter));

            return GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            Check.NotNull(filter, nameof(filter));

            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            return GetQueryable(null, orderBy, includeProperties, skip, take).ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            return await GetQueryable(null, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public T GetById(object id)
        {
            Check.NotNull(id, nameof(id));

            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            Check.NotNull(id, nameof(id));

            return await _context.Set<T>().FindAsync(id);
        }

        public T GetFirst(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            return GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        public T GetSingle(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            return GetQueryable(filter, null, includeProperties).SingleOrDefault();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            return await GetQueryable(filter, null, includeProperties).SingleOrDefaultAsync();
        }

        protected virtual IQueryable<T> GetQueryable(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            includeProperties ??= string.Empty;
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }
    }
}