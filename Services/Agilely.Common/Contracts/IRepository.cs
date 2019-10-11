using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Agilely.Common.Contracts
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T : class
    {
        /// <summary>
        /// Creates a new <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">The new instance of <paramref name="entity"/> to be created</param>
        /// <returns>Persisted <paramref name="entity"/></returns>
        T Create(T entity);

        /// <summary>
        /// Creates a new <paramref name="entity"/> asynchronously
        /// </summary>
        /// <param name="entity">The new instance of <paramref name="entity"/> to be created</param>
        /// <returns>Persisted <paramref name="entity"/></returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Deletes collection of <paramref name="entity"/> matching <paramref name="filter"/>
        /// </summary>
        /// <param name="filter">Expression which supplies all desired filters to be deleted</param>
        void Delete(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Deletes collection of <paramref name="entity"/> matching <paramref name="filter"/> asynchronously
        /// </summary>
        /// <param name="filter">Expression which supplies all desired filters to be deleted</param>
        Task DeleteAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Deletes an entity with a given primary key.
        /// </summary>
        /// <param name="id">The primary key of the entity to be deleted</param>
        void Delete(object id);

        /// <summary>
        /// Deletes an entity with a given primary key
        /// </summary>
        /// <param name="id">The primary key of the entity to be deleted</param>
        Task DeleteAsync(object id);

        /// <summary>
        /// Deletes an existing <paramref name="entity"/> asynchronously
        /// </summary>
        /// <param name="entity">The entity instance to be deleted</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes an existing <paramref name="entity"/> asynchronously
        /// </summary>
        /// <param name="entity">The entity instance to be deleted</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Updates an existing <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">The entity instance to be updated</param>
        /// <returns>Updated entity</returns>
        T Update(T entity);

        /// <summary>
        /// Updates an existing <paramref name="entity"/> asynchronously
        /// </summary>
        /// <param name="entity">The entity instance to be updated</param>
        /// <returns>Updated entity</returns>
        Task<T> UpdateAsync(T entity);
    }
}