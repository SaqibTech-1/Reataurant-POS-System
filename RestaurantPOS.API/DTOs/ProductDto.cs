using RestaurantPOS.API.Entities;

namespace RestaurantPOS.API.DTOs
{
    public class ProductDto : BaseEntity
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool IsAvailable { get; set; }
    }
}
