using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressPermissions
{
    public class PermissionGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
