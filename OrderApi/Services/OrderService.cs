using OrderApi.Infrastructure.Repositories;
using OrderApi.Infrastructure.UnitOfWork;
using OrderApi.Models;

namespace OrderApi.Services
{
    public interface IOrderService
    {
        Task Add(Order order);
        void Update(Order order);
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

        public async Task Add(Order order)
        {
            await _orderRepository.AddAsync(order);
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
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
