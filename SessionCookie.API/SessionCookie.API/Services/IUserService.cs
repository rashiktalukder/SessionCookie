using SessionCookie.API.DTOs;

namespace SessionCookie.API.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegistrationRequest userRegistrationRequest);
    }
}
