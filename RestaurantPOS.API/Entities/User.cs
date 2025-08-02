namespace RestaurantPOS.API.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public bool IsActive { get; set; }

    }
}
