using GIS_API.DBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS_API.DataRepositories.Privileges
{
    public interface IPrivilegesRepository
    {
        public Task<Privilege> AddPrivilege(Privilege privilege);

        public Task<Privilege> GetPrivilege(int id);

        public Task<List<Privilege>> GetAllPrivileges();

        public void DeletePrivilege(Privilege privilege);

        public Task<List<Privilege>> GetPrivilegesForObject(string objectType, int objectId);

        public void DeletePrivileges(List<Privilege> privileges);
    }
}
