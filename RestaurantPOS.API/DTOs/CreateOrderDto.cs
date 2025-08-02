namespace RestaurantPOS.API.DTOs
{
    public class CreateOrderDto
    {
        public Guid? GlobalId { get; set; }
        public int TableId { get; set; }
        public int UserId { get; set; }
        public List<CreateOrderItemDto>? Items { get; set; }
    }
}
