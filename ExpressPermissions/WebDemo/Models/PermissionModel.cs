using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Models
{
    public class PermissionModel
    {
        public string Username { get; set; }
        public string[] Permissions { get; set; }
    }
}
