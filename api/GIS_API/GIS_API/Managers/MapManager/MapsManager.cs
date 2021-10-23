using AutoMapper;
using GIS_API.DataRepositories.Maps;
using GIS_API.DataRepositories.RolePrivileges;
using GIS_API.DBModels;
using GIS_API.Enums;
using GIS_API.Managers.Privileges;
using GIS_API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS_API.Managers.MapManager
{
    public class MapsManager : IMapsManager
    {
        private readonly IMapper mapper;
        private readonly IMapsRepository mapRepository;
        private readonly IRolePrivilegesRepository rolePrivilegesRepository;
        private readonly IPrivilegesManager privilegeManager;

        public MapsManager(IMapper mapper,
                          IMapsRepository mapRepository,
                          IRolePrivilegesRepository rolePrivilegesRepository,
                          IPrivilegesManager privilegeManager)
        {
            this.mapper = mapper;
            this.mapRepository = mapRepository;
            this.rolePrivilegesRepository = rolePrivilegesRepository;
            this.privilegeManager = privilegeManager;
        }


        public async Task<Map> AddNewMap(MapViewModel model)
        {
            var domainModel = mapper.Map<Map>(model);
            var result = await this.mapRepository.AddMap(domainModel);
            return result;
        }

        public async Task DeleteMap(int id)
        {
            var map = await this.mapRepository.GetMap(id);
            var relatedPrivileges = await this.privilegeManager.GetPrivilegesForObject(ObjectType.Map, map.Id);
            this.privilegeManager.DeletePrivileges(relatedPrivileges);
            this.rolePrivilegesRepository.DeleteRelatedToPrivilege(id);
            this.mapRepository.DeleteMap(map);
        }

        public async Task<List<Map>> GetAllMaps()
        {
            return await this.mapRepository.GetAllMaps();
        }

        public async Task<Map> GetMap(int id)
        {
            return await this.mapRepository.GetMap(id);
        }
    }
}
