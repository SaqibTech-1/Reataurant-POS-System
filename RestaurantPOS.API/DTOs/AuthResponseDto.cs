namespace RestaurantPOS.API.DTOs
{
    public class AuthResponseDto
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
