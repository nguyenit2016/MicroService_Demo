using OrderApi.Infrastructure.Repositories;
using OrderApi.Infrastructure.UnitOfWork;
using OrderApi.Models;

namespace OrderApi.Services
{
    public interface IOrderService
    {
        void Update(Order product);
        IEnumerable<Order> GetAll();
        Task<Order?> GetByIdAsync(int id);
        void SaveChanges();
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public void Update(Order product)
        {
            _orderRepository.Update(product);
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChangesAsync();
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAllAsync().Result;
        }

        public Task<Order?> GetByIdAsync(int id)
        {
            return _orderRepository.GetByIdAsync(id);
        }
    }
}
