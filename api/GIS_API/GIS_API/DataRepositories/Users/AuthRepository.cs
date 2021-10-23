using AutoMapper;
using GIS_API.DBModels;
using GIS_API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GIS_API.DataRepositories.Users
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public AuthRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<User> Login(string email, string password)
        {
            var model = await this.context.users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            return model;
        }

        public async Task<User> Register(User user)
        {
            var model = mapper.Map<User>(user);
            await this.context.users.AddAsync(model);
            await this.context.SaveChangesAsync();
            return model;
        }
    }
}
