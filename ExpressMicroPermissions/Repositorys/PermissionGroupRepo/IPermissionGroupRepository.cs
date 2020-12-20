using ExpressMicroPermissions.Models;
using ExpressMicroPermissions.Repositorys.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressMicroPermissions.Repositorys.PermissionGroupRepo
{
    public interface IPermissionGroupRepository : IRepository<PermissionGroup>
    {
        PermissionGroup GetPermissionGroupByName(string name);
        IEnumerable<PermissionGroup> GetAllPermissionGroups();
        bool IsPermissionGroupExist(string groupName);
    }
}
