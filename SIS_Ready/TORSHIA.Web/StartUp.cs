using SIS.Framework.Api;
using SIS.Framework.Services;
using TORSHIA.Web.Services;
using TORSHIA.Web.Services.Contracts;

namespace TORSHIA.Web
{
    public class StartUp : MvcApplication
    {
        public virtual void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<IUsersService, UsersServices>();
            dependencyContainer.RegisterDependency<ITasksService, TasksService>();
            dependencyContainer.RegisterDependency<IReportsService, ReportsService>();
        }
    }
}