using System.Threading.Tasks;
using GIS_API.DBModels;
using Microsoft.EntityFrameworkCore;

namespace GIS_API.DataRepositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;
        
        public UserRepository(DataContext context)
        {
            this.context = context;
        }
        
        public async Task<User> GetUserById(int userId)
        {
            return await this.context.users.FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
