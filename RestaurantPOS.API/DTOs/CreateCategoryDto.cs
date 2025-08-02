namespace RestaurantPOS.API.DTOs
{
    public class CreateCategoryDto
    {
        public Guid? GlobalId { get; set; } = Guid.Empty;
        public string? Name { get; set; }
    }
}
