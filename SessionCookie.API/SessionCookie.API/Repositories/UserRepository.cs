using Microsoft.EntityFrameworkCore;
using SessionCookie.API.DBContext;
using SessionCookie.API.Models;

namespace SessionCookie.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext db)
        {
            _dbContext = db;
        }
        public async Task AddUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string userName)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName)??new User();
        }

        public async Task<bool> IsUserNameTakenAsync(string userName)
        {
            return await _dbContext.Users.AnyAsync(x=>x.UserName == userName);
        }
    }
}
