using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusStationCRM.DAL.Contexts;
using BusStationCRM.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusStationCRM.DAL.Repositories
{
    //Base class for repositories
    public class BaseRepository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _entities;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task<IAsyncEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate)/////////
        {
            return (await GetAllAsync()).Where(predicate).ToAsyncEnumerable();
        }

        public virtual async Task<TEntity> GetAsync(int id) ////////////////////////////////////maybe override
        {
            return await _entities.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()/////////////////////////// should be override
        {
            return await _entities.AsQueryable().ToListAsync();
        }

        public async Task CreateAsync(TEntity item)
        {
            await _entities.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _entities.FindAsync(id);

            if (entity == null)
            {
                throw new ArgumentException($"Error! {typeof(TEntity)} not deleted, because not found!");
            }

            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
