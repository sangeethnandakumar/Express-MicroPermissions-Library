using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressPermissions
{
    public class PermissionInfo
    {
		public int Id { get; set; }
		public string Permission { get; set; }
		public string Description { get; set; }
		public bool? IsEnabled { get; set; }
		public string VersionSupport { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? Updated { get; set; }
		public DateTime? Deleted { get; set; }
	}
}
