namespace SensorProcessing.DataAccess.Repository
{
    public interface IEntityRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}
