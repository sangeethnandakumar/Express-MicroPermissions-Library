using ExpressPermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Models
{
    public class ViewModal
    {
        public IEnumerable<PermissionInfo> AllPermissions { get; set; }
        public IEnumerable<PermissionGroup> AllPermissionGroups { get; set; }
        public IEnumerable<PermissionUser> AllUsers { get; set; }
    }
}
