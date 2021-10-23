using System.Threading.Tasks;

namespace GIS_API.PrivilegesChecker
{
    public interface IAccessChecker
    {
        Task<bool> CheckUserAccessForLayer(string token, string privilegeType, string layerName);

        Task<bool> CheckUserAccessForMap(string token, string privilegeType, string layerName);

        Task<bool> CheckForAdminPrivileges(string token);
    }
}
