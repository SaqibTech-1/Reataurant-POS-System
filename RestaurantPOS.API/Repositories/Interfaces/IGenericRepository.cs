
using RestaurantPOS.API.Entities;

namespace RestaurantPOS.API.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByGlobalIdAsync(Guid globalId);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
