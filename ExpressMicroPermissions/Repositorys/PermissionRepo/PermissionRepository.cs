using ExpressMicroPermissions.Data;
using ExpressMicroPermissions.Models;
using ExpressMicroPermissions.Repositorys.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressMicroPermissions.Repositorys.PermissionRepo
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(PermissionContext context) : base(context) { }
        public PermissionContext PermissionContext { get { return (PermissionContext)context;  } }

        public IEnumerable<Permission> GetPaginatedPermissions(int index, int size)
        {
            return PermissionContext.Permissions
                .OrderBy(x => x.Name)
                .Skip((index - 1) * size)
                .Take(size)
                .ToList();
        }

        public Permission GetPermissionByName(string name)
        {
            return PermissionContext.Permissions
                .Where(x => x.Name == name)
                .FirstOrDefault();
        }

        public bool IsPermissionExist(string permissionName)
        {
            var permission = PermissionContext.Permissions
                .Where(x => x.Name == permissionName)
                .FirstOrDefault();
            return permission!=null? true : false;
        }
    }
}
