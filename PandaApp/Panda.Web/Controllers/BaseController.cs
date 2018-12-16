using Panda.Data;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Web.Controllers
{
    public class BaseController : Controller
    {
        protected BaseController()
        {
            this.Db = new PandaDbContext();
        }

        public PandaDbContext Db { get; }
    }
}
