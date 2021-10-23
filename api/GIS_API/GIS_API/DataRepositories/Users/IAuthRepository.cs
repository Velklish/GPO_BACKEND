using GIS_API.DBModels;
using System.Threading.Tasks;

namespace GIS_API.DataRepositories.Users
{
    public interface IAuthRepository
    {
        Task<User> Register(User user);

        Task<User> Login(string email, string password);
    }
}