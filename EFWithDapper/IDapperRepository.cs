namespace EFWithDapper
{
    public interface IDapperRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(int id);
        Task AddAsync<T>(T entity);
        Task UpdateAsync<T>(T entity);
        Task DeleteAsync<T>(int id);
    }

}
