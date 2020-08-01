## EnabledAll()
Checks if all given permissions are available to a user
```csharp
var username = "sangee";
permission.EnabledAll(username, "p.AddUser", "p.EditUser", "p.DeleteUser")
```
## EnabledAny()
Checks if atleast one permissions is available to a user
```csharp
var username = "sangee";
permission.EnabledAny(username, "p.AddUser", "p.EditUser", "p.DeleteUser")
```
## BindPermission()
Adds a new permission to user if not exists
```csharp
var username = "sangee";
permission.BindPermission(username, "p.RevokeAccess");
```
## UnBindPermission()
Removes permission from user if exists
```csharp
var username = "sangee";
permission.UnBindPermission(username, "p.RevokeAccess");
```
## GetPermission()
Gives information about a specific permission
```csharp
var info = permission.GetPermission("p.RevokeAccess");
```
## AllPermissions()
Gives a list of all permission informations
```csharp
var info = permission.AllPermissions();
```
## EnabledPermissions()
Gives a list of all binded permissions of a user
```csharp
var username = "sangee";
permission.EnabledPermissions(username);
```
## DisabledPermissions()
Gives a list of all non-binded permissions of a user
```csharp
var username = "sangee";
permission.DisabledPermissions(username);
```
## CreatePermissionGroup()
Creates a new permission group if not exists
```csharp
var permisionGroup = "g.SuperAdmin";
permission.CreatePermissionGroup(permisionGroup);
```
## BindPermissionToPermissionsGroup()
Binds a new permission to an existing permission group
```csharp
var permission = "p.AddUser";
var permisionGroup = "g.SuperAdmin";
permission.BindPermissionToPermissionsGroup(permisionGroup, permission);
```
## UnBindPermissionFromPermissionGroup()
UnBinds an existing permission from a permission group
```csharp
var permission = "p.AddUser";
var permisionGroup = "g.SuperAdmin";
permission.UnBindPermissionFromPermissionGroup(permisionGroup, permission);
```
## GetPermisionGroups()
Gives a list of all permission groups available
```csharp
var result = permission.GetPermisionGroups();
```
## GetPermissionsFromPermissionGroup()
Gives a list of permissions contained in a permission group
```csharp
var permisionGroup = "g.SuperAdmin";
var result = permission.GetPermissionsFromPermissionGroup(permisionGroup);
```
## BindPermissionGroup()
Binds all permissions inside a permission group to a user
```csharp
var username = "sangee";
var permisionGroup = "g.SuperAdmin";
var result = permission.BindPermissionGroup(username, permisionGroup);
```
## UnBindPermissionGroup()
UnBinds all permissions inside a permission group from a user
```csharp
var username = "sangee";
var permisionGroup = "g.SuperAdmin";
var result = permission.UnBindPermissionGroup(username, permisionGroup);
```
