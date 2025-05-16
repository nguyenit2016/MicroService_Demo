using Confluent.Kafka;
using ProductApi.Models;

namespace ProductApi.Kafka
{
    public class KafkaConsumer(IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                _ = ConsumerAsync("order-topic", stoppingToken);
            }, stoppingToken);
        }
        private async Task ConsumerAsync(string topic, CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "product-service-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using (var consumer = new ConsumerBuilder<string, string>(config).Build())
            {
                consumer.Subscribe(topic);
                var _productDbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ProductDbContext>();
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var cr = consumer.Consume(stoppingToken);
                        // Process the message
                        Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                        // Here you can add logic to update the product stock in your database
                        var product = await _productDbContext.Products.FindAsync(cr.Key);
                        if (product != null)
                        {
                            product.Stock -= 1; // Giả sử đặt 1 sản phẩm, mai mốt lấy từ message (quantity)
                            await _productDbContext.SaveChangesAsync();
                        }
                        consumer.Commit(cr);
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occurred: {e.Error.Reason}");
                    }
                }
            }
        }
    }
}
