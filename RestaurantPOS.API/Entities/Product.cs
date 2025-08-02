using Microsoft.EntityFrameworkCore;

namespace RestaurantPOS.API.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsAvailable { get; set; }
    }
}
