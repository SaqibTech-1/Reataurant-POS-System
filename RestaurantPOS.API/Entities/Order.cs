using Microsoft.EntityFrameworkCore;

namespace RestaurantPOS.API.Entities
{
    public class Order : BaseEntity
    {
        public int TableId { get; set; }
        public Table? Table { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime? OrderTime { get; set; } = DateTime.UtcNow;
        public ICollection<OrderItem>? Items { get; set; }
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
    }
}
