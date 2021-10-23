using AutoMapper;
using GIS_API.DBModels;
using GIS_API.ViewModels;

namespace GIS_API
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();
            
            CreateMap<RoleViewModel, Role>();
            CreateMap<Role, RoleViewModel>();

            CreateMap<PrivilegeViewModel, Privilege>();
            CreateMap<Privilege, PrivilegeViewModel>();

            CreateMap<MapViewModel, Map>();
            CreateMap<Map, MapViewModel>();
        }
    }
}
