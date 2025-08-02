namespace RestaurantPOS.API.DTOs
{
    public class CreateProductDto
    {
        public Guid? GlobalId { get; set; } = Guid.Empty;
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
