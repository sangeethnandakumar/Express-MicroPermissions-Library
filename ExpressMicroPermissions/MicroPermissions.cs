using ExpressMicroPermissions.Data;
using ExpressMicroPermissions.Models;
using ExpressMicroPermissions.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressMicroPermissions
{
    public static class MicroPermissions
    {
        public static bool AddPermission(string permissionName, string description, PermissionLevel permissionLevel)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (!unitOfWork.Permissions.IsPermissionExist(permissionName))
                {
                    unitOfWork.Permissions.Add(new Permission
                    {
                        Name = permissionName,
                        Description = description,
                        IsEnabled = true,
                        PermissionLevel = permissionLevel
                    });
                    unitOfWork.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool RemovePermission(string permissionName)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (unitOfWork.Permissions.IsPermissionExist(permissionName))
                {
                    var permissionToRemove = unitOfWork.Permissions.GetPermissionByName(permissionName);
                    var allPermisiongroups = unitOfWork.PermissionGroups.GetAllPermissionGroups();
                    foreach (var group in allPermisiongroups)
                    {
                        group.Permissions.Remove(permissionToRemove);
                    }
                    unitOfWork.Permissions.Remove(permissionToRemove);
                    unitOfWork.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool AddPermissionGroup(string groupName)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (!unitOfWork.PermissionGroups.IsPermissionGroupExist(groupName))
                {
                    unitOfWork.PermissionGroups.Add(new PermissionGroup
                    {
                        Name = groupName,
                        IsEnabled = true
                    });
                    unitOfWork.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool RemovePermissionGroup(string groupName)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (unitOfWork.PermissionGroups.IsPermissionGroupExist(groupName))
                {
                    var group = unitOfWork.PermissionGroups.GetPermissionGroupByName(groupName);
                    unitOfWork.PermissionGroups.Remove(group);
                    unitOfWork.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool HasAllPermissions(int userId, params string[] permissionNames)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (unitOfWork.UserPermissions.IsUserExist(userId))
                {
                    foreach (var permisisonName in permissionNames)
                    {
                        if (unitOfWork.Permissions.IsPermissionExist(permisisonName))
                        {
                            var permission = unitOfWork.Permissions.GetPermissionByName(permisisonName);
                            var existingRecord = unitOfWork.UserPermissions.Find(x => x.UserId == userId && x.PermissionId == permission.Id).FirstOrDefault();
                            if (existingRecord == null)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }                        
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool HasAnyPermission(int userId, params string[] permissionNames)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (unitOfWork.UserPermissions.IsUserExist(userId))
                {
                    foreach(var permisisonName in permissionNames)
                    {
                        if(unitOfWork.Permissions.IsPermissionExist(permisisonName))
                        {
                            var permission = unitOfWork.Permissions.GetPermissionByName(permisisonName);
                            var existingRecord = unitOfWork.UserPermissions.Find(x => x.UserId == userId && x.PermissionId == permission.Id).FirstOrDefault();
                            if (existingRecord != null)
                            {
                                return true;
                            }
                            return false;
                        }
                        else
                        {
                            return false;
                        }                        
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static bool IsAllowed(int userId, string permissionName)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (unitOfWork.UserPermissions.IsUserExist(userId) && unitOfWork.Permissions.IsPermissionExist(permissionName))
                {
                    var permission = unitOfWork.Permissions.GetPermissionByName(permissionName);
                    var existingRecord = unitOfWork.UserPermissions.Find(x => x.UserId == userId && x.PermissionId == permission.Id).FirstOrDefault();
                    if (existingRecord != null)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool AddPermissionToPermissionGroup(string groupName, string permissionName)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                var isPermissionGroupExist = unitOfWork.PermissionGroups.IsPermissionGroupExist(groupName);
                var isPermissionExist = unitOfWork.Permissions.IsPermissionExist(permissionName);
                var existingPermissions = unitOfWork.PermissionGroups.GetPermissionGroupByName(groupName).Permissions.ToList();
                if (isPermissionGroupExist && isPermissionExist && (existingPermissions.Find(x => x.Name == permissionName) == null))
                {
                    var group = unitOfWork.PermissionGroups.GetPermissionGroupByName(groupName);
                    var permission = unitOfWork.Permissions.GetPermissionByName(permissionName);
                    group.Permissions.Add(permission);
                    unitOfWork.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool RemovePermissionFromPermissionGroup(string groupName, string permissionName)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                var isPermissionGroupExist = unitOfWork.PermissionGroups.IsPermissionGroupExist(groupName);
                var isPermissionExist = unitOfWork.Permissions.IsPermissionExist(permissionName);
                if (isPermissionGroupExist && isPermissionExist)
                {
                    var group = unitOfWork.PermissionGroups.GetPermissionGroupByName(groupName);
                    var permission = unitOfWork.Permissions.GetPermissionByName(permissionName);
                    group.Permissions.Remove(permission);
                    unitOfWork.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static List<string> GetPermissionsFromPermissionGroup(string groupName)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (unitOfWork.PermissionGroups.IsPermissionGroupExist(groupName))
                {
                    var permissions = unitOfWork.PermissionGroups.GetPermissionGroupByName(groupName).Permissions.Select(x => x.Name).ToList();
                    return permissions;
                }
                else
                {
                    return new List<string>();
                }
            }
        }

        public static List<string> GetPermissionsFromPermissionGroups(string[] groupNames)
        {
            List<string> result = new List<string>();
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                foreach (var group in groupNames)
                {
                    if (unitOfWork.PermissionGroups.IsPermissionGroupExist(group))
                    {
                        var permissions = unitOfWork.PermissionGroups.GetPermissionGroupByName(group).Permissions.Select(x => x.Name).ToList();
                        result = result.Union(permissions).ToList();
                    }
                }
            }
            return result;
        }

        public static bool BindPermissionToUser(int userId, string permissionName)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (unitOfWork.Permissions.IsPermissionExist(permissionName))
                {
                    var permission = unitOfWork.Permissions.GetPermissionByName(permissionName);
                    var existingRecord = unitOfWork.UserPermissions.Find(x => x.UserId == userId && x.PermissionId == permission.Id).FirstOrDefault();
                    if (existingRecord == null)
                    {
                        unitOfWork.UserPermissions.Add(new UserPermission { UserId = userId, PermissionId = permission.Id });
                        unitOfWork.Complete();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool UnBindPermissionFromUser(int userId, string permissionName)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (unitOfWork.Permissions.IsPermissionExist(permissionName))
                {
                    var permission = unitOfWork.Permissions.GetPermissionByName(permissionName);
                    var existingRecord = unitOfWork.UserPermissions.Find(x => x.UserId == userId && x.PermissionId == permission.Id).FirstOrDefault();
                    if (existingRecord != null)
                    {
                        unitOfWork.UserPermissions.Remove(existingRecord);
                        unitOfWork.Complete();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool BindPermissionGroupToUser(int userId, string permissionGroupName)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (unitOfWork.PermissionGroups.IsPermissionGroupExist(permissionGroupName))
                {
                    var permissionGroup = unitOfWork.PermissionGroups.GetPermissionGroupByName(permissionGroupName);
                    foreach(var permisison in permissionGroup.Permissions)
                    {
                        BindPermissionToUser(userId, permisison.Name);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool UnBindPermissionGroupFromUser(int userId, string permissionGroupName)
        {
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (unitOfWork.PermissionGroups.IsPermissionGroupExist(permissionGroupName))
                {
                    var permissionGroup = unitOfWork.PermissionGroups.GetPermissionGroupByName(permissionGroupName);
                    foreach (var permisison in permissionGroup.Permissions)
                    {
                        UnBindPermissionFromUser(userId, permisison.Name);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static List<string> GetPermissionsOfUser(int userId)
        {
            List<string> result = new List<string>();
            using (var unitOfWork = new UnitOfWork(new PermissionContext()))
            {
                if (unitOfWork.UserPermissions.IsUserExist(userId))
                {
                    var userPermissions = unitOfWork.UserPermissions.Find(x=>x.UserId==userId).ToList();
                    foreach (var userPermission in userPermissions)
                    {
                        result.Add(unitOfWork.Permissions.Get(userPermission.PermissionId).Name);
                    }
                    return result;
                }
                else
                {
                    return result;
                }
            }
        }



    }

}
