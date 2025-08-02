namespace RestaurantPOS.API.DTOs
{
    public class CreateUserDto
    {
        public Guid? GlobalId { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
    }
}
