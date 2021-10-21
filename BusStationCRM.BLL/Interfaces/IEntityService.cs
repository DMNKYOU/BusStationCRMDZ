using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BusStationCRM.BLL.Interfaces
{
    public interface IEntityService<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);//
        Task EditAsync(TEntity entity); //
        Task DeleteAsync(int id);
        Task<IAsyncEnumerable<TEntity>> FindAsync(Func<TEntity, Boolean> predicate);
    }
}