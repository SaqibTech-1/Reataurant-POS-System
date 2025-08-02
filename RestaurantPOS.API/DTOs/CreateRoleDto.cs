namespace RestaurantPOS.API.DTOs
{
    public class CreateRoleDto
    {
        public Guid? GlobalId { get; set; } = Guid.Empty;
        public string? Name { get; set; }
    }
}
