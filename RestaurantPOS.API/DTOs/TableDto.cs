using RestaurantPOS.API.Entities;

namespace RestaurantPOS.API.DTOs
{
    public class TableDto : BaseEntity
    {
        public string? TableNumber { get; set; }
        public bool IsOccupied { get; set; }
    }
}
