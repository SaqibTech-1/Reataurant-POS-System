namespace RestaurantPOS.API.Services.GetCurrentUser
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
    }
}
