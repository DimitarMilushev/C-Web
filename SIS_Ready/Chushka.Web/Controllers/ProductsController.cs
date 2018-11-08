using Chushka.Web.Controllers.Base;
using Chushka.Web.Services;
using Chushka.Web.Services.Contracts;
using Chushka.Web.ViewModels;
using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Action;
using SIS.Framework.Attributes.Method;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductsService service;

        public ProductsController(ProductsService service)
        {
            this.service = service;

        }

        [Authorize("Admin")]
        public IActionResult Create() => this.View();

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Create(ProductInputViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
