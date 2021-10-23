using GIS_API.DBModels;
using GIS_API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS_API.Managers.Privileges
{
    public interface IPrivilegesManager
    {
        public Task<Privilege> AddNewPrivilege(PrivilegeViewModel model);

        public Task DeletePrivilege(int id);

        public Task<List<Privilege>> GetAllPrivileges();

        public Task<Privilege> GetPrivilege(int id);

        public Task<List<Privilege>> GetPrivilegesForObject(string objectType, int objectId);

        public void DeletePrivileges(List<Privilege> privileges);
    }
}
