using ProductApi.Infrastructure.Repositories;
using ProductApi.Infrastructure.UnitOfWork;
using ProductApi.Models;

namespace ProductApi.Services
{
    public interface IProductService
    {
        void Update(Product product);
        IEnumerable<Product> GetAll();
        Task<Product?> GetByIdAsync(int id);
        void SaveChanges();
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChangesAsync();
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAllAsync().Result;
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            return _productRepository.GetByIdAsync(id);
        }
    }
}
