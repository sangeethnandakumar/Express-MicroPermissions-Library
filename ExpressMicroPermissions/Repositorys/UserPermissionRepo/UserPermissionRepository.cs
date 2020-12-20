using ExpressMicroPermissions.Data;
using ExpressMicroPermissions.Models;
using ExpressMicroPermissions.Repositorys.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressMicroPermissions.Repositorys.UserRepo
{
    public class UserPermissionRepository : Repository<UserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(PermissionContext context) : base(context) { }
        public PermissionContext PermissionContext { get { return (PermissionContext)context; } }

        public bool IsUserExist(int userId)
        {
            var user = PermissionContext.UserPermissions.Where(x => x.UserId == userId).FirstOrDefault();
            return user != null ? true : false;
        }
    }
}
