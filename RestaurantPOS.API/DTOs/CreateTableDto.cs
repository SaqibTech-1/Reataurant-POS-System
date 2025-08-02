namespace RestaurantPOS.API.DTOs
{
    public class CreateTableDto
    {
        public Guid? GlobalId { get; set; } = Guid.Empty;
        public string? TableNumber { get; set; }
        public bool IsOccupied { get; set; }
    }
}
