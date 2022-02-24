using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Context;
using Movies.DataAccess.Repositories.Interfaces;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movies.DataAccess.Repositories.Implementation
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly MovieContext _movieContext;
        private DbSet<TEntity> _entities;

        public BaseRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
            _entities = movieContext.Set<TEntity>();
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                return false;
            }
            await _entities.AddAsync(entity);
            return await _movieContext.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync() => await _entities.CountAsync();

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            _entities.Remove(entity);
            return await _movieContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _entities.AnyAsync(expression);
        }

        public async Task<IEnumerable<TEntity>> FilterAsync(Func<TEntity, bool> expression)
        {
            return _entities.Where(expression);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _movieContext.Update(entity);
            return await _movieContext.SaveChangesAsync() > 0;
        }
    }
}