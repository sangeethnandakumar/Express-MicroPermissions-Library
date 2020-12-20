using ExpressMicroPermissions.Data;
using ExpressMicroPermissions.Models;
using ExpressMicroPermissions.Repositorys.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressMicroPermissions.Repositorys.PermissionGroupRepo
{
    public class PermissionGroupRepository : Repository<PermissionGroup>, IPermissionGroupRepository
    {
        public PermissionContext PermissionContext { get { return (PermissionContext)context; } }
        public PermissionGroupRepository(PermissionContext context) : base(context) { }

        public PermissionGroup GetPermissionGroupByName(string name)
        {
            return PermissionContext.PermissionGroups
                .Include(x=>x.Permissions)
                .Where(x => x.Name == name)
                .FirstOrDefault();
        }

        public bool IsPermissionGroupExist(string groupName)
        {
            var group =  PermissionContext.PermissionGroups
                .Where(x => x.Name == groupName)
                .FirstOrDefault();
            return group != null ? true : false;
        }

        public IEnumerable<PermissionGroup> GetAllPermissionGroups()
        {
            var groups = PermissionContext.PermissionGroups
                .Include(x=>x.Permissions)
                .ToList();
            return groups;
        }
    }
}
