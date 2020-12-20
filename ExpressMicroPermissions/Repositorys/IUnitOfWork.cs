using ExpressMicroPermissions.Repositorys.PermissionGroupRepo;
using ExpressMicroPermissions.Repositorys.PermissionRepo;
using ExpressMicroPermissions.Repositorys.UserRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressMicroPermissions.Repositorys
{
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository Permissions { get; }
        IPermissionGroupRepository PermissionGroups { get; }
        IUserPermissionRepository UserPermissions { get; }
        int Complete();
    }
}
