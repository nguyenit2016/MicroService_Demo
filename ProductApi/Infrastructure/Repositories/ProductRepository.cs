using ProductApi.Models;

namespace ProductApi.Infrastructure.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        // Add custom methods for Entity here if needed
    }
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ProductDbContext context) : base(context)
        {

        }
    }
}
