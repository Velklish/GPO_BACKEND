using GIS_API.DBModels;
using GIS_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GIS_API.Managers.Users
{
    public interface IUserManager
    {
        Task<User> Register(UserViewModel user);

        Task<User> Login(LoginViewModel user);

        Task SetRoles(List<int> roleIds, int userId);

        Task AddRoles(List<int> roleIds, int userId);
    }
}
