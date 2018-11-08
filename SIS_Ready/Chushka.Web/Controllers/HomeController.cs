using Chushka.Web.Controllers.Base;
using Chushka.Web.Services;
using Chushka.Web.Services.Contracts;
using Chushka.Web.ViewModels;
using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chushka.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductsService service;

        public HomeController(ProductsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (this.Identity == null)
                return this.View();

            var products = this.service.All().ToList();
            var wrapperViewModel = new List<ProductViewModelWrapper>();
            wrapperViewModel.Add(new ProductViewModelWrapper());

            ProductViewModelWrapper lastAddedWrapper = new ProductViewModelWrapper();

            for (int i = 0; i < products.Count(); i++)
            {
                if (i % 5 == 0)
                {
                    wrapperViewModel.Add(new ProductViewModelWrapper());
                    lastAddedWrapper = wrapperViewModel.Last();

                }

                lastAddedWrapper.ProductViewModels.Add(new ProductViewModel
                {
                    Id = products[i].Id,
                    Title = products[i].Name,
                    Price = products[i].Price,
                    Description = products[i].Description
                });
            }

            this.Model.Data["ProductViewModels"] = wrapperViewModel;

            return this.View();
        }
    }
}
