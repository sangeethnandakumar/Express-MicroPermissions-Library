From the StartUp project, Add

```csharp
public void ConfigureServices(IServiceCollection services)
        {
            //Set connection string for MicroPermissions Store
            services.AddDbContext<PermissionContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("JetTask"),
                assembly => assembly.MigrationsAssembly(typeof(PermissionContext).Assembly.FullName));
            });
            
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddLiveReload();
        }
```


AddPermission(string permissionName, string description, PermissionLevel permissionLevel)

RemovePermission(string permissionName)

AddPermissionGroup(string groupName)

RemovePermissionGroup(string groupName)

HasAllPermissions(int userId, params string[] permissionNames)

HasAnyPermission(int userId, params string[] permissionNames)

IsAllowed(int userId, string permissionName)

AddPermissionToPermissionGroup(string groupName, string permissionName)

RemovePermissionFromPermissionGroup(string groupName, string permissionName)

GetPermissionsFromPermissionGroup(string groupName)

GetPermissionsFromPermissionGroups(string[] groupNames)

BindPermissionToUser(int userId, string permissionName)

UnBindPermissionFromUser(int userId, string permissionName)

BindPermissionGroupToUser(int userId, string permissionGroupName)

UnBindPermissionGroupFromUser(int userId, string permissionGroupName)

GetPermissionsOfUser(int userId)
