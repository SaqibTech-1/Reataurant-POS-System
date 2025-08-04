using RestaurantPOS.API.Entities;

namespace RestaurantPOS.API.DTOs
{
    public class ProductDto : BaseEntity
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsAvailable { get; set; }
        public string? FoodType { get; set; }
        public string? ImageUrl { get; set; }
    }
}
