using SIS.MvcFramework;
using SIS.MvcFramework.Services;
using System;

namespace Panda.Web
{
    public class StartUp : IMvcApplication
    {
        public MvcFrameworkSettings Configure()
        {
            return new MvcFrameworkSettings();
        }

        public void ConfigureServices(IServiceCollection collection)
        {
        }
    }
}
