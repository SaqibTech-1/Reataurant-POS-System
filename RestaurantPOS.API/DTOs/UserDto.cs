using RestaurantPOS.API.Entities;

namespace RestaurantPOS.API.DTOs
{
    public class UserDto : BaseEntity
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public int RoleId { get; set; }
    }
}
