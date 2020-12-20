using ExpressMicroPermissions.Data;
using ExpressMicroPermissions.Repositorys.PermissionGroupRepo;
using ExpressMicroPermissions.Repositorys.PermissionRepo;
using ExpressMicroPermissions.Repositorys.UserRepo;

namespace ExpressMicroPermissions.Repositorys
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPermissionRepository Permissions { get; private set; }
        public IPermissionGroupRepository PermissionGroups { get; private set; }
        public IUserPermissionRepository UserPermissions { get; private set; }

        private readonly PermissionContext context;

        public UnitOfWork(PermissionContext context)
        {
            this.context = context;
            Permissions = new PermissionRepository(context);
            PermissionGroups = new PermissionGroupRepository(context);
            UserPermissions = new UserPermissionRepository(context);
        }        

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
