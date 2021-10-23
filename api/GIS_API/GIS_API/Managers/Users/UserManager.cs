using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GIS_API.DataRepositories.UserRoles;
using GIS_API.DataRepositories.Users;
using GIS_API.DBModels;
using GIS_API.ViewModels;
using System.Threading.Tasks;

namespace GIS_API.Managers.Users
{
    public class UserManager : IUserManager
    {
        private readonly IMapper mapper;
        private readonly IAuthRepository authRepository;
        private readonly IUserRolesRepository userRoleRepository;

        public UserManager(IAuthRepository authRepository, IMapper mapper, IUserRolesRepository userRoleRepository)
        {
            this.authRepository = authRepository;
            this.mapper = mapper;
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<User> Login(LoginViewModel user)
        {
            return await this.authRepository.Login(user.Email, user.Password);
        }

        public async Task SetRoles(List<int> roleIds, int userId)
        {
            this.userRoleRepository.DeleteRelatedToUser(userId);
            var userRoles = roleIds.Select(x => new UserRole() {RoleId = x, UserId = userId}).ToList();
            await this.userRoleRepository.AddUserRoles(userRoles);
        }

        public async Task AddRoles(List<int> roleIds, int userId)
        {
            var userRoles = await this.userRoleRepository.GetUserRoles(userId);
            var rolesToAdd = roleIds.Except(userRoles.Select(x => x.Id))
                .Select(roleId => new UserRole(){RoleId = roleId, UserId = userId}).ToList();
            await this.userRoleRepository.AddUserRoles(rolesToAdd);
        }

        public async Task<User> Register(UserViewModel user)
        {
            var domainModel = this.mapper.Map<User>(user);
            var result = await this.authRepository.Register(domainModel);
            foreach (var roleId in user.RolesId)
            {
                await this.userRoleRepository.AddUserRole(new UserRole
                {
                    RoleId = roleId,
                    UserId = result.Id
                });
            }

            return result;
        }
    }
}
