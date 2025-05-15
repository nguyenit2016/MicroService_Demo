namespace WebApp.Data
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        // public string ImageUrl { get; set; }
    }
}
