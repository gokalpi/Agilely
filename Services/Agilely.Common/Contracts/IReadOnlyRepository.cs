using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Agilely.Common.Contracts
{
    public interface IReadOnlyRepository<T> where T : class
    {
        /// <summary>
        /// Gets all records of <typeparamref name="T"/> including <paramref name="includeProperties"/> with paging support
        /// </summary>
        /// <param name="orderBy">Expression which supplies all ordering criteria</param>
        /// <param name="includeProperties">Navigation properties that should be included</param>
        /// <param name="skip">Number of records to skip in resultset for paging</param>
        /// <param name="take">Number of records in a page</param>
        /// <returns>All records of <typeparamref name="T"/> including <paramref name="includeProperties"/> with paging support</returns>
        IEnumerable<T> GetAll(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        /// <summary>
        /// Gets all records of <typeparamref name="T"/> including <paramref name="includeProperties"/> with paging support asynchronously
        /// </summary>
        /// <param name="orderBy">Expression which supplies all ordering criteria</param>
        /// <param name="includeProperties">Navigation properties that should be included</param>
        /// <param name="skip">Number of records to skip in resultset for paging</param>
        /// <param name="take">Number of records in a page</param>
        /// <returns>All records of <typeparamref name="T"/> including <paramref name="includeProperties"/> with paging support</returns>
        Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        /// <summary>
        /// Gets records of <typeparamref name="T"/> matching <paramref name="filter"/> including <paramref name="includeProperties"/> with paging support
        /// </summary>
        /// <param name="filter">Expression which supplies all desired filters.</param>
        /// <param name="orderBy">Expression which supplies all ordering criteria</param>
        /// <param name="includeProperties">Navigation properties that should be included</param>
        /// <param name="skip">Number of records to skip in resultset for paging</param>
        /// <param name="take">Number of records in a page</param>
        /// <returns>All records of <typeparamref name="T"/> matching <paramref name="filter"/> including <paramref name="includeProperties"/> with paging support</returns>
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        /// <summary>
        /// Gets records of <typeparamref name="T"/> matching <paramref name="filter"/> including <paramref name="includeProperties"/> with paging support asynchronously
        /// </summary>
        /// <param name="filter">Expression which supplies all desired filters.</param>
        /// <param name="orderBy">Expression which supplies all ordering criteria</param>
        /// <param name="includeProperties">Navigation properties that should be included</param>
        /// <param name="skip">Number of records to skip in resultset for paging</param>
        /// <param name="take">Number of records in a page</param>
        /// <returns>All records of <typeparamref name="T"/> matching <paramref name="filter"/> including <paramref name="includeProperties"/> with paging support</returns>
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        /// <summary>
        /// Gets an entity instance based on its primary key
        /// If no entity is found, then default(T) is returned.
        /// </summary>
        /// <param name="id">The desired entity key value</param>
        /// <returns>The entity matching the specified <paramref name="id"/> if it exists.</returns>
        T GetById(object id);

        /// <summary>
        /// Gets an entity instance based on its primary key asynchronously
        /// If no entity is found, then default(T) is returned.
        /// </summary>
        /// <param name="id">The desired entity key value</param>
        /// <returns>The entity matching the specified <paramref name="id"/> if it exists.</returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// Gets the first entity instance based on <paramref name="filter"/>
        /// If no entity is found, then default(T) is returned.
        /// </summary>
        /// <param name="filter">Expression which supplies all desired filters.</param>
        /// <param name="orderBy">Expression which supplies all ordering criteria</param>
        /// <param name="includeProperties">Navigation properties that should be included</param>
        /// <returns>The first entity matching the specified <paramref name="filter"/> if it exists</returns>
        T GetFirst(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);

        /// <summary>
        /// Gets the first entity instance based on <paramref name="filter"/> asynchronously
        /// If no entity is found, then default(T) is returned.
        /// </summary>
        /// <param name="filter">Expression which supplies all desired filters.</param>
        /// <param name="orderBy">Expression which supplies all ordering criteria</param>
        /// <param name="includeProperties">Navigation properties that should be included</param>
        /// <returns>The first entity matching the specified <paramref name="filter"/> if it exists</returns>
        Task<T> GetFirstAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);

        /// <summary>
        /// Gets the single entity instance based on <paramref name="filter"/>
        /// If no entity is found, then default(T) is returned.
        /// If more than one entity is found, then invalid operation exception is thrown.
        /// </summary>
        /// <param name="filter">Expression which supplies all desired filters.</param>
        /// <param name="includeProperties">Navigation properties that should be included</param>
        /// <returns>Single element of <see cref="T"/>, or default(T) if the result contains no elements.</returns>
        T GetSingle(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null);

        /// <summary>
        /// Gets the first entity instance based on <paramref name="filter"/> asynchronously
        /// If no entity is found, then default(T) is returned.
        /// If more than one entity is found, then invalid operation exception is thrown.
        /// </summary>
        /// <param name="filter">Expression which supplies all desired filters.</param>
        /// <param name="includeProperties">Navigation properties that should be included</param>
        /// <returns>Single element of <see cref="T"/>, or default(T) if the result contains no elements.</returns>
        Task<T> GetSingleAsync(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null);

        /// <summary>
        /// Counts existing entities filtered by given <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter">Expression which supplies all desired filters.</param>
        /// <returns>Number of entities found based on the criteria</returns>
        int Count(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Counts existing entities filtered by given <paramref name="filter"/> asynchronously
        /// </summary>
        /// <param name="filter">Expression which supplies all desired filters.</param>
        /// <returns>Number of entities found based on the criteria</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Checks if an entity exists matching the specified <paramref name="filter"/>
        /// </summary>
        /// <param name="filter">Expression which supplies all desired filters.</param>
        /// <returns>True if entity exists, otherwise false</returns>
        bool Exists(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Checks if an entity exists matching the specified <paramref name="filter"/> asynchronously
        /// </summary>
        /// <param name="filter">Expression which supplies all desired filters.</param>
        /// <returns>True if entity exists, otherwise false</returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter = null);
    }
}