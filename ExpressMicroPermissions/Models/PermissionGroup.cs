using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressMicroPermissions.Models
{
    public class PermissionGroup
    {
        public PermissionGroup()
        {
            Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public bool IsEnabled { get; set; }
    }
}
