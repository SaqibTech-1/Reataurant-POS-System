namespace RestaurantPOS.API.Entities
{
    public class Table : BaseEntity
    {
        public string? TableNumber { get; set; }
        public bool IsOccupied { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
