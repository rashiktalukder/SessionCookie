using SessionCookie.API.Models;

namespace SessionCookie.API.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsUserNameTakenAsync(string userName);
        Task<User> GetUserByUsernameAsync(string userName);
        Task AddUserAsync(User user);
    }
}
