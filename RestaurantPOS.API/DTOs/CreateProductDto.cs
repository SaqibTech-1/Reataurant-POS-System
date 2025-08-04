namespace RestaurantPOS.API.DTOs
{
    public class CreateProductDto
    {
        public Guid? GlobalId { get; set; } = Guid.Empty;
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsAvailable { get; set; }        
        public string? FoodType { get; set; }
        public IFormFile? Image { get; set; }
    }
}
