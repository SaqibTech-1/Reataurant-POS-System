using Microsoft.EntityFrameworkCore;
using RestaurantPOS.API.Data;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Repositories.Interfaces;

namespace RestaurantPOS.API.Repositories.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbSet.Where(e => !e.IsDeleted).ToListAsync();

        public async Task<T> GetByIdAsync(int id) =>
            await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

        public async Task<T> GetByGlobalIdAsync(Guid globalId) =>
            await _dbSet.FirstOrDefaultAsync(e => e.GlobalId == globalId && !e.IsDeleted);

        public async Task AddAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public void Update(T entity) =>
            _dbSet.Update(entity);

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.IsActive = false;
            _dbSet.Update(entity);
        }
    }

}
