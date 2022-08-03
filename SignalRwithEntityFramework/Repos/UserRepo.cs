using Microsoft.EntityFrameworkCore;
using SignalRwithEntityFramework.Models;

namespace SignalRwithEntityFramework.Repos
{
    public class UserRepo
    {
        private readonly SignalRWithEFContext dbContext;   

        public UserRepo(SignalRWithEFContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> GetUserDetails(string username, string password)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Username == username && user.Password == password);
        }
    }
}
