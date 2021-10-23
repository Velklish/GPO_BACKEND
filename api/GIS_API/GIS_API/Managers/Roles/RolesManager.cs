using AutoMapper;
using GIS_API.DataRepositories.RolePrivileges;
using GIS_API.DataRepositories.Roles;
using GIS_API.DBModels;
using GIS_API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS_API.Managers.Roles
{
    public class RolesManager : IRolesManager
    {
        private readonly IMapper mapper;
        private readonly IRolePrivilegesRepository rolePrivilegesRepository;
        private readonly IRolesRepository rolesRepository;

        public RolesManager(IMapper mapper,
                          IRolePrivilegesRepository rolePrivilegesRepository,
                          IRolesRepository rolesRepository)
        {
            this.mapper = mapper;
            this.rolePrivilegesRepository = rolePrivilegesRepository;
            this.rolesRepository = rolesRepository;
        }

        public async Task<Role> AddNewRole(RoleViewModel model)
        {
            var domainModel = this.mapper.Map<Role>(model);
            var result = await this.rolesRepository.AddRole(domainModel);
            var rolePrivileges = new List<RolePrivilege>();
            foreach(var rolePrivilege in model.PrivilegesIds)
            {
                rolePrivileges.Add(new RolePrivilege
                {
                    RoleId = result.Id,
                    PrivilegeId = rolePrivilege
                });
            }

            await this.rolePrivilegesRepository.AddRolePrivileges(rolePrivileges);
            return result;
        }

        public async Task DeleteRole(int id)
        {
            var role = await this.rolesRepository.GetRole(id);
            this.rolePrivilegesRepository.DeleteRelatedToRole(id);
            this.rolesRepository.DeleteRole(role);
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await this.rolesRepository.GetRoles();
        }

        public async Task<Role> GetRole(int id)
        {
            return await this.rolesRepository.GetRole(id);
        }
    }
}
