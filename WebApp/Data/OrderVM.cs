namespace WebApp.Data
{
    public class OrderVM
    {
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
    }
}
