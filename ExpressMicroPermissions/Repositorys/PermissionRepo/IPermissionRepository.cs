using ExpressMicroPermissions.Models;
using ExpressMicroPermissions.Repositorys.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressMicroPermissions.Repositorys.PermissionRepo
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        IEnumerable<Permission> GetPaginatedPermissions(int index, int size);
        Permission GetPermissionByName(string name);
        bool IsPermissionExist(string permissionName);
    }
}
