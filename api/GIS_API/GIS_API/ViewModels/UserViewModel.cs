using System.Collections.Generic;

namespace GIS_API.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        
        public bool IsAdmin { get; set; }
        
        public List<int> RolesId { get; set; }
    }
}