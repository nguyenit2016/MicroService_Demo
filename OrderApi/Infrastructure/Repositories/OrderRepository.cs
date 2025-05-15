using OrderApi.Models;

namespace OrderApi.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        // Add custom methods for Entity here if needed
    }
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext context) : base(context)
        {

        }
    }
}
