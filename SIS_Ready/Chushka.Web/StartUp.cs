using Chushka.Web.Services;
using Chushka.Web.Services.Contracts;
using SIS.Framework.Api;
using SIS.Framework.Services;

namespace Chushka.Web
{
    public class StartUp : MvcApplication
    {
        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<IUsersService, UsersService>();
            dependencyContainer.RegisterDependency<IProductsService, ProductsService>();
        }
    }
}
