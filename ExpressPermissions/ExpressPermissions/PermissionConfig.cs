using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressPermissions
{
    public class PermissionConfig
    {
        public string UserDBConnectionString { get; set; }
        public string PermissionDBConnectionString { get; set; }
        public string UserDBName { get; set; }
        public string UserTable { get; set; }
        public string UserIdColumn { get; set; }
        public string UsernameColumn { get; set; }
        public string UserFullNameQuery { get; set; }
    }
}
