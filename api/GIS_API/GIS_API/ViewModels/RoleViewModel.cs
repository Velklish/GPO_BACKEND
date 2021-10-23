using System.Collections.Generic;

namespace GIS_API.ViewModels
{
    public class RoleViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public List<int> PrivilegesIds { get; set; }
    }
}
