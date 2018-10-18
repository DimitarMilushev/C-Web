using SIS.Framework.ActionResults.Base;
using SIS.Framework.Controllers.Base;

namespace IRunesWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
