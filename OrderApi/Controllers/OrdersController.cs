using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Infrastructure.Repositories;
using OrderApi.Infrastructure.UnitOfWork;
using OrderApi.Kafka;
using OrderApi.Models;
using OrderApi.Services;
using OrderApi.ViewModel;
using System.Text.Json;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKafkaProducer _kafkaProducer;
        public OrdersController(IOrderService orderService, IUnitOfWork unitOfWork, IKafkaProducer kafkaProducer)
        {
            _orderService = orderService;
            _unitOfWork = unitOfWork;
            _kafkaProducer = kafkaProducer;
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(_orderService.GetAll());
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            if (ModelState.IsValid)
            {
                Order order = new Order
                {
                    UserId = 1,
                    Total = orderRequest.Price * orderRequest.Quantity,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = orderRequest.ProductId,
                            Quantity = orderRequest.Quantity,
                            UnitPrice = orderRequest.Price
                        }
                    }
                };
                await _orderService.Add(order);
                await _unitOfWork.SaveChangesAsync();

                // send order to queue
                var message = new Message<string, string>
                {
                    Key = orderRequest.ProductId.ToString(),
                    Value = JsonSerializer.Serialize(order)
                };
                await _kafkaProducer.ProduceAsync("order-topic", message);

                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
