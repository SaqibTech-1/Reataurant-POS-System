using System.Security.Claims;

namespace RestaurantPOS.API.Services.GetCurrentUser
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //public int? GetUserId()
        //{
        //    var user = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    return int.TryParse(user,out var id) ? id : 0;
        //}

        public int? GetUserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                return userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
            }
        }

       
    }
}
