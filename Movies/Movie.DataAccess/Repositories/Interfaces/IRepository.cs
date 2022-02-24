using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movies.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<bool> AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(Guid id);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<IEnumerable<TEntity>> FilterAsync(Func<TEntity, bool> expression);

        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression);

        Task<int> CountAsync();
    }
}