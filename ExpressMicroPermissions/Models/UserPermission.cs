using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpressMicroPermissions.Models
{
    public class UserPermission
    {
        public int UserId { get; set; }
        public int PermissionId { get; set; }
    }
}
