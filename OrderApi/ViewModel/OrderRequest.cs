namespace OrderApi.ViewModel
{
    public class OrderRequest
    {
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
    }
}
