using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressMicroPermissions.Models
{
    public class Permission
    {
        public Permission()
        {
            PermissionGroups = new HashSet<PermissionGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public PermissionLevel PermissionLevel { get; set; }
        public ICollection<PermissionGroup> PermissionGroups { get; set; }
    }
}
