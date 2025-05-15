using OrderApi.Models;

namespace OrderApi.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> SaveChangesAsync();
    }
    public class UnitOfWork : IUnitOfWork
    {

        private readonly OrderDbContext _context;

        public UnitOfWork(OrderDbContext context)
        {
            _context = context;
        }
        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransaction()
        {
            await _context.Database.CommitTransactionAsync();
        }
        public async Task RollBack()
        {
            await _context.Database.RollbackTransactionAsync();
        }
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
