using Microsoft.EntityFrameworkCore;

namespace SensorProcessing.DataAccess.Repository
{
    public class EntityRepository<T>(SensorProcessingDbContext context) : IEntityRepository<T> where T : class
    {
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }   

        public async Task Delete(Guid id)
        {   
            var entity = await GetByIdAsync(id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
