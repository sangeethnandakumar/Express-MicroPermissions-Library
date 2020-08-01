using ExpressData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressPermissions
{
    public class Permissions
    {
        private PermissionArgs _args;
        private string Username { get; set; }

        public Permissions(PermissionArgs args)
        {
            _args = args;
            SetUpInfrastructure();
        }

        public Permissions()
        {
        }

        public void SetUpInfrastructure()
        {
        }

        //Returns TRUE is all permissions are satisfied
        public bool EnabledAll(string username, params string[] permissions)
        {
            bool isAllowed = true;

            if (_args != null)
            {
                foreach (var permission in permissions)
                {
                    var query = @"SELECT COUNT(A.Id) FROM tblPermissions A
                                INNER JOIN tblPermissionBindings B ON B.PermissionId = A.Id
                                INNER JOIN "+ _args.UserDBName + @"..tblUser C ON C.Id=B.UserId
                                WHERE C.FName='" + username + "' AND A.IsEnabled=1 AND B.IsEnabled=1 AND A.Permission='" + permission + "'";
                    var resut = SqlHelper.Query<int>(query, _args.PermissionDBConnectionString).FirstOrDefault();
                    if (resut < 1)
                    {
                        isAllowed = false;
                    }
                }
                return isAllowed;
            }

            return true;
        }

        //Returns TRUE if atleast one permission is satisfied
        public bool EnabledAny(string username, params string[] permissions)
        {
            bool isAllowed = true;

            if (_args != null)
            {
                var inStatement = String.Empty;
                for (var i = 0; i < permissions.Length; i++)
                {
                    if (i < permissions.Length - 1)
                    {
                        inStatement += "'" + permissions[i] + "', ";
                    }
                    else
                    {
                        inStatement += "'" + permissions[i] + "'";
                    }
                }
                var query = @"SELECT COUNT(A.Id) FROM tblPermissions A
                                INNER JOIN tblPermissionBindings B ON B.PermissionId = A.Id
                                INNER JOIN tblUser C ON C.Id=B.UserId
                                WHERE C.FName='" + username + "' AND A.IsEnabled=1 AND B.IsEnabled=1 AND A.Permission IN (" + inStatement + ")";
                var resut = SqlHelper.Query<int>(query, _args.UserDBConnectionString).FirstOrDefault();
                if (resut < 1)
                {
                    isAllowed = false;
                }
                return isAllowed;
            }

            return true;
        }

        //Add a new permission if not exist
        public bool BindPermission(string username, string permission)
        {
            if (_args != null)
            {

                var query = $"SELECT TOP(1) {_args.UserIdColumn} FROM {_args.UserTable} WHERE {_args.UsernameColumn}='{username}'";
                var userId = SqlHelper.Query<int>(query, _args.UserDBConnectionString).FirstOrDefault();

                query = $"SELECT TOP(1) Id FROM tblPermissions WHERE Permission='{permission}'";
                var permissionId = SqlHelper.Query<int>(query, _args.UserDBConnectionString).FirstOrDefault();

                query = $"SELECT COUNT(PermissionId) FROM tblPermissionBindings WHERE UserId={userId} AND PermissionId={permissionId}";
                var isAlreadyExist = SqlHelper.Query<int>(query, _args.UserDBConnectionString).FirstOrDefault();

                if (userId > 0 && permissionId > 0 && isAlreadyExist <= 0)
                {
                    query = $"INSERT INTO tblPermissionBindings (UserId, PermissionId, IsEnabled, LastBinded) VALUES ({userId},{permissionId},1,GETUTCDATE())";
                    SqlHelper.Query<int>(query, _args.UserDBConnectionString);
                    return true;
                }
                return false;
            }
            return true;
        }

        //Removes permission if exist
        public bool UnBindPermission(string username, string permission)
        {
            if (_args != null)
            {

                var query = $"SELECT TOP(1) {_args.UserIdColumn} FROM {_args.UserTable} WHERE {_args.UsernameColumn}='{username}'";
                var userId = SqlHelper.Query<int>(query, _args.UserDBConnectionString).FirstOrDefault();

                query = $"SELECT TOP(1) Id FROM tblPermissions WHERE Permission='{permission}'";
                var permissionId = SqlHelper.Query<int>(query, _args.UserDBConnectionString).FirstOrDefault();

                query = $"SELECT COUNT(PermissionId) FROM tblPermissionBindings WHERE UserId={userId} AND PermissionId={permissionId}";
                var isAlreadyExist = SqlHelper.Query<int>(query, _args.UserDBConnectionString).FirstOrDefault();

                if (userId > 0 && permissionId > 0 && isAlreadyExist > 0)
                {
                    query = $"DELETE FROM tblPermissionBindings WHERE UserId={userId} AND PermissionId={permissionId}";
                    SqlHelper.Query<int>(query, _args.UserDBConnectionString);
                    return true;
                }
                return false;
            }
            return true;
        }

        //Get permission details
        public PermissionInfo GetPermission(string permission)
        {
            if (_args != null)
            {

                var query = $"SELECT TOP(1) * FROM tblPermissions WHERE Permission='{permission}'";
                var permissionInfo = SqlHelper.Query<PermissionInfo>(query, _args.UserDBConnectionString).FirstOrDefault();
                return permissionInfo;
            }
            return new PermissionInfo();
        }

        //Get all Permission
        public IEnumerable<PermissionInfo> AllPermissions()
        {
            if (_args != null)
            {
                var query = $"SELECT * FROM tblPermissions WHERE IsEnabled=1";
                var permissionInfos = SqlHelper.Query<PermissionInfo>(query, _args.UserDBConnectionString);
                return permissionInfos;
            }
            return new List<PermissionInfo>();
        }

        //Get enabled Permission of a user
        public IEnumerable<PermissionInfo> EnabledPermissions(string username)
        {
            if (_args != null)
            {
                var query = @"SELECT A.* FROM tblPermissions A
                            INNER JOIN tblPermissionBindings B ON B.PermissionId = A.Id
                            INNER JOIN " + _args.UserTable + @" C ON C." + _args.UserIdColumn + @"=B.UserId
                            WHERE C.FName='" + username + "' AND A.IsEnabled=1 AND B.IsEnabled=1";
                var permissionInfos = SqlHelper.Query<PermissionInfo>(query, _args.UserDBConnectionString);
                return permissionInfos;
            }
            return new List<PermissionInfo>();
        }

        //Get disabled Permission of a user
        public IEnumerable<PermissionInfo> DisabledPermissions(string username)
        {
            if (_args != null)
            {
                var query = @"SELECT A.* FROM tblPermissions A WHERE Id NOT IN(
                            SELECT B.PermissionId FROM tblPermissionBindings B
                            INNER JOIN " + _args.UserTable + @" C ON C." + _args.UserIdColumn + @"=B.UserId
                            WHERE C.FName='" + username + @"' AND B.IsEnabled=1
                            )";
                var permissionInfos = SqlHelper.Query<PermissionInfo>(query, _args.UserDBConnectionString);
                return permissionInfos;
            }
            return new List<PermissionInfo>();
        }

        //Check if a permission exists
        public bool Exist(string permission)
        {
            if (_args != null)
            {
                var query = $"SELECT COUNT(Id) FROM tblPermissions WHERE Permission='{permission}'";
                var isExist = SqlHelper.Query<int>(query, _args.PermissionDBConnectionString).FirstOrDefault();
                if(isExist>0)
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        //Check if a permission exists inside a permission group
        public bool ExistInPermissionGroup(string name, string permission)
        {
            if (_args != null)
            {
                var query = @"SELECT COUNT(A.Id) FROM tblPermissionGroups A
                            INNER JOIN tblPermissionGroupBindings B ON A.Id=B.PermissionGroupId
                            INNER JOIN tblPermissions C ON C.Id=B.PermisssionId
                            WHERE A.Name='" + name +"' AND C.Permission='"+ permission +"' AND C.IsEnabled=1";
                var isExist = SqlHelper.Query<int>(query, _args.PermissionDBConnectionString).FirstOrDefault();
                if (isExist > 0)
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        //Create a permission group
        public bool CreatePermissionGroup(string name)
        {
            if (_args != null)
            {
                var query = $"SELECT COUNT(Id) FROM tblPermissionGroups WHERE Name='{name}'";
                var isExist = SqlHelper.Query<int>(query, _args.UserDBConnectionString).FirstOrDefault();
                if (isExist < 1)
                {
                    SqlHelper.Query<int>($"INSERT INTO tblPermissionGroups (Name, Created) VALUES ('{name}', GETUTCDATE())", _args.PermissionDBConnectionString);
                    return true;
                }
                return false;
            }
            return false;
        }

        //Add permissions to permission group
        public bool BindPermissionToPermissionsGroup(string name, string permission)
        {
            if (_args != null)
            {
                var query = $"SELECT COUNT(Id) FROM tblPermissionGroups WHERE Name='{name}'";
                var isExist = SqlHelper.Query<int>(query, _args.PermissionDBConnectionString).FirstOrDefault();
                if (isExist > 0 && Exist(permission) && !ExistInPermissionGroup(name, permission))
                {
                    query = $"SELECT TOP(1) Id FROM tblPermissionGroups WHERE Name='{name}'";
                    var permissionGroupId = SqlHelper.Query<int>(query, _args.PermissionDBConnectionString).FirstOrDefault(); 
                    query = $"SELECT TOP(1) Id FROM tblPermissions WHERE Permission='{permission}'";
                    var permissionId = SqlHelper.Query<int>(query, _args.PermissionDBConnectionString).FirstOrDefault();

                    SqlHelper.Query<int>($"INSERT INTO tblPermissionGroupBindings (PermissionGroupId, PermisssionId) VALUES ({permissionGroupId},{permissionId})", _args.PermissionDBConnectionString);
                    return true;
                }
                return false;
            }
            return false;
        }

        //Remove permission from permission group
        public bool UnBindPermissionFromPermissionGroup(string name, string permission)
        {
            if (_args != null)
            {
                var query = $"SELECT COUNT(Id) FROM tblPermissionGroups WHERE Name='{name}'";
                var isExist = SqlHelper.Query<int>(query, _args.PermissionDBConnectionString).FirstOrDefault();
                if (isExist > 0 && Exist(permission) && ExistInPermissionGroup(name, permission))
                {
                    query = $"SELECT TOP(1) Id FROM tblPermissionGroups WHERE Name='{name}'";
                    var permissionGroupId = SqlHelper.Query<int>(query, _args.PermissionDBConnectionString).FirstOrDefault();
                    query = $"SELECT TOP(1) Id FROM tblPermissions WHERE Permission='{permission}'";
                    var permissionId = SqlHelper.Query<int>(query, _args.PermissionDBConnectionString).FirstOrDefault();

                    SqlHelper.Query<int>($"DELETE FROM tblPermissionGroupBindings WHERE PermissionGroupId={permissionGroupId} AND PermisssionId={permissionId}", _args.PermissionDBConnectionString);
                    return true;
                }
                return false;
            }
            return false;
        }

        //List all permission group
        public IEnumerable<PermissionGroup> GetPermisionGroups()
        {
            if (_args != null)
            {
                var query = "SELECT * FROM tblPermissionGroups";
                var result = SqlHelper.Query<PermissionGroup>(query, _args.UserDBConnectionString);
                return result;
            }
            return new List<PermissionGroup>();
        }

        //List all permissions under a permission group
        public IEnumerable<PermissionInfo> GetPermissionsFromPermissionGroup(string name)
        {
            if (_args != null)
            {
                var query = @"SELECT C.* FROM tblPermissionGroups A
                            INNER JOIN tblPermissionGroupBindings B ON B.PermissionGroupId=A.Id
                            INNER JOIN tblPermissions C ON C.Id=B.PermisssionId
                            WHERE A.Name='"+ name +"' AND C.IsEnabled=1";
                var result = SqlHelper.Query<PermissionInfo>(query, _args.UserDBConnectionString);
                return result;
            }
            return new List<PermissionInfo>();
        }

        //Bind a permission group to a user
        public bool BindPermissionGroup(string username, string name)
        {
            if (_args != null)
            {
                var groupPermissions = GetPermissionsFromPermissionGroup(name);
                var userPermissions = EnabledPermissions(username);

                var requiredPermissions = groupPermissions.Where(g => userPermissions.All(p => p.Id != g.Id));

                foreach (var permission in requiredPermissions)
                {
                    BindPermissions(username, permission.Permission);
                }             
                
                return true;
            }
            return false;
        }

        //Unbind a permission group from a user
        public bool UnBindPermissionGroup(string username, string name)
        {
            if (_args != null)
            {
                if (_args != null)
                {
                    var groupPermissions = GetPermissionsFromPermissionGroup(name);

                    foreach (var permission in groupPermissions)
                    {
                        UnBindPermissions(username, permission.Permission);
                    }

                    return true;
                }
                return false;
            }
            return false;
        }

    }
}
