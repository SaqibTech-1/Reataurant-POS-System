using RestaurantPOS.API.Data;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Repositories.Implementation;
using RestaurantPOS.API.Repositories.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private IGenericRepository<Category> _categories;
        public IGenericRepository<Category> Categories =>
            _categories ??= new GenericRepository<Category>(_context);

        private IGenericRepository<Product> _products;
        public IGenericRepository<Product> Products =>
            _products ??= new GenericRepository<Product>(_context);

        private IGenericRepository<User> _users;
        public IGenericRepository<User> Users =>
            _users ??= new GenericRepository<User>(_context);

        private IGenericRepository<Role> _roles;
        public IGenericRepository<Role> Roles =>
            _roles ??= new GenericRepository<Role>(_context);

        private IGenericRepository<Table> _tables;
        public IGenericRepository<Table> Tables =>
            _tables ??= new GenericRepository<Table>(_context);

        private IGenericRepository<Order> _orders;
        public IGenericRepository<Order> Orders =>
            _orders ??= new GenericRepository<Order>(_context);

        private IGenericRepository<OrderItem> _orderItems;
        public IGenericRepository<OrderItem> OrderItems =>
            _orderItems ??= new GenericRepository<OrderItem>(_context);
        public async Task<int> SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public void Dispose() =>
            _context.Dispose();
    }
}
