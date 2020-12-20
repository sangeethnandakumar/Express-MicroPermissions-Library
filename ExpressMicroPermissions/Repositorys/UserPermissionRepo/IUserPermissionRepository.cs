using ExpressMicroPermissions.Models;
using ExpressMicroPermissions.Repositorys.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressMicroPermissions.Repositorys.UserRepo
{
    public interface IUserPermissionRepository : IRepository<UserPermission>
    {
        public bool IsUserExist(int userId);
    }
}
