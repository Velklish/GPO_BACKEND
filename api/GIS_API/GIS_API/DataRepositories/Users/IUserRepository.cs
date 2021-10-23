using System.Threading.Tasks;
using GIS_API.DBModels;

namespace GIS_API.DataRepositories.Users
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int userId);
    }
}
