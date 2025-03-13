using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IInterface<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
        Task<TEntity> GetEntityByIdAsync(int id);
        Task AddEntityAsync(TEntity entity);
        Task UpdateEntityAsync(TEntity entity);
        Task DeleteEntityAsync(int id);
    }
}
