using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repasitories
{
    public class Repasitory<TEntity> : IInterface<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _appDbContext;

        public Repasitory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddEntityAsync(TEntity entity)
        => await _appDbContext.Set<TEntity>().AddAsync(entity);

        public async Task DeleteEntityAsync(int id)
        {
            var entity = await _appDbContext.Set<TEntity>().FindAsync(id);
            _appDbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync()
        {
           return await _appDbContext.Set<TEntity>()
                            .AsNoTracking()
                            .ToListAsync();
        }

        public async Task<TEntity> GetEntityByIdAsync(int id)
        => await _appDbContext.Set<TEntity>().FindAsync(id);

        public async Task UpdateEntityAsync(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Update(entity); 
        }
    }
}
