using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using GIS_API.DataRepositories.Layers;
using GIS_API.DataRepositories.RolePrivileges;
using GIS_API.DataRepositories.UserRoles;
using GIS_API.DataRepositories.Users;
using GIS_API.Enums;
using GIS_API.Managers.Privileges;
using static System.Int32;

namespace GIS_API.PrivilegesChecker
{
    public class AccessChecker : IAccessChecker
    {
        private readonly IUserRolesRepository userRolesRepository;
        private readonly IRolePrivilegesRepository rolePrivilegesRepository;
        private readonly IUserRepository userRepository;
        private readonly IPrivilegesManager privilegesManager;
        private readonly ILayersRepository layersRepository;

        public AccessChecker(IUserRolesRepository userRolesRepository,
            IRolePrivilegesRepository rolePrivilegesRepository,
            IUserRepository userRepository, 
            IPrivilegesManager privilegesManager, 
            ILayersRepository layersRepository)
        {
            this.userRolesRepository = userRolesRepository;
            this.rolePrivilegesRepository = rolePrivilegesRepository;
            this.userRepository = userRepository;
            this.privilegesManager = privilegesManager;
            this.layersRepository = layersRepository;
        }

        public async Task<bool> CheckUserAccessForLayer(string token, string privilegeType, string layerName)
        {
            if (await this.CheckForAdminPrivileges(token))
            {
                return true;
            }
            
            int userId = this.GetCurrentUserId(token);
            var roles = await this.userRolesRepository.GetUserRoles(userId);
            var layer = await this.layersRepository.GetLayer(layerName);

            List<int> privilegesIdList = new List<int>();

            foreach (var role in roles)
            {
                var privileges = this.rolePrivilegesRepository.GetRolePrivileges(role.RoleId).Result.Select(x => x.PrivilegeId);
                privilegesIdList.AddRange(privileges);
            }

            var privilegesForObject =
                (await this.privilegesManager.GetPrivilegesForObject(ObjectType.Layer, layer.Id))
                .Where(x => x.PrivilegeType == privilegeType).ToList();

            var result = privilegesForObject.Select(x => x.Id).Intersect(privilegesIdList.Distinct()).Any();
            return result;
        }

        public Task<bool> CheckUserAccessForMap(string token, string privilegeType, string layerName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckForAdminPrivileges(string token)
        {
            int userId = this.GetCurrentUserId(token);
            var user = await this.userRepository.GetUserById(userId);
            return user.IsAdmin;
        }

        private int GetCurrentUserId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            var claim = jwtToken.Claims.First(x => x.Type == "nameid").Value;
            return Parse(claim);
        }
    }
}
