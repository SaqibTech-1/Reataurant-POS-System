using RestaurantPOS.API.Entities;

namespace RestaurantPOS.API.DTOs
{
    public class OrderDto : BaseEntity
    {
        public int TableId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public List<OrderItemDto>? OrderItems { get; set; }
    }
}
