using AutoMapper;
using GIS_API.DataRepositories.Privileges;
using GIS_API.DataRepositories.RolePrivileges;
using GIS_API.DBModels;
using GIS_API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS_API.Managers.Privileges
{
    public class PrivilegesManager : IPrivilegesManager
    {
        private readonly IPrivilegesRepository privilegesRepository;
        private readonly IMapper mapper;
        private readonly IRolePrivilegesRepository rolePrivilegeRepository;

        public PrivilegesManager(
            IPrivilegesRepository privilegesRepository, 
            IMapper mapper, 
            IRolePrivilegesRepository rolePrivilegeRepository)
        {
            this.privilegesRepository = privilegesRepository;
            this.mapper = mapper;
            this.rolePrivilegeRepository = rolePrivilegeRepository;
        }

        public async Task<Privilege> AddNewPrivilege(PrivilegeViewModel model)
        {
            var domainModel = mapper.Map<Privilege>(model);
            return await this.privilegesRepository.AddPrivilege(domainModel);
        }

        public async Task DeletePrivilege(int id)
        {
            var privilege = await this.privilegesRepository.GetPrivilege(id);
            this.rolePrivilegeRepository.DeleteRelatedToPrivilege(id);
            this.privilegesRepository.DeletePrivilege(privilege);
        }

        public void DeletePrivileges(List<Privilege> privileges)
        {
            foreach(var privilege in privileges) 
            {
                this.rolePrivilegeRepository.DeleteRelatedToPrivilege(privilege.Id);
            }

            this.privilegesRepository.DeletePrivileges(privileges);
        }

        public async Task<List<Privilege>> GetAllPrivileges()
        {
            return await this.privilegesRepository.GetAllPrivileges();
        }

        public async Task<Privilege> GetPrivilege(int id)
        {
            return await this.privilegesRepository.GetPrivilege(id);
        }

        public async Task<List<Privilege>> GetPrivilegesForObject(string objectType, int objectId)
        {
            return await this.privilegesRepository.GetPrivilegesForObject(objectType, objectId);
        }
    }
}
