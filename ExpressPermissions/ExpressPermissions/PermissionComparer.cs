using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ExpressPermissions
{
    public class PermissionComparer : IEqualityComparer<PermissionInfo>
    {
        public bool Equals([AllowNull] PermissionInfo x, [AllowNull] PermissionInfo y)
        {
            if (x.Id == y.Id) { return true; }
            return false;
        }

        public int GetHashCode([DisallowNull] PermissionInfo obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
