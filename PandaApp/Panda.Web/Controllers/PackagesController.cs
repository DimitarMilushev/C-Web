using Panda.Models.Enums;
using Panda.Web.ViewModels.Packages;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panda.Web.Controllers
{
    public class PackagesController : BaseController
    {
        [Authorize]
        public IHttpResponse Details(int id)
        {
            var viewModel = this.Db.Packages
                .Select(x => new PackageDetailsViewModel
                {
                    Address = x.ShippingAddress,
                    Status = x.Status.ToString(),
                    Weight = x.Weight,
                    EstimatedDeliveryDate = GetDeliveryDate(x.EstimatedDeliveryDate, x.Status),
                    Recipient = x.Recipient.Username,
                    Description = x.Description
                })
                .FirstOrDefault(x => x.Id == id);

            if (viewModel == null)
            {
                return this.BadRequestError("Invalid product id.");
            }

            return this.View(viewModel);
        }

        [Authorize("Admin")]
        public IHttpResponse Create() => this.View();

        [Authorize("Admin")]
        public IHttpResponse Pending()
        {
            var pendingPackages = this.Db.Packages.Select(x =>
            new PackageViewModel
            {
                Id = x.Id,
                Weight = x.Weight,
                Status = nameof(x.Status),
                ShippingAddress = x.ShippingAddress,
                Description = x.Description,
                Recipient = x.Recipient.Username
            }).ToList();

            if (pendingPackages.Count == 0)
                return this.BadRequestError("There are no packages");

            var model = new AllPackagesViewModel { Packages = pendingPackages };
            return this.View(model);
        }

        private string GetDeliveryDate(DateTime? estimatedDeliveryDate, Status status)
        {
            switch (status)
            {
                case Status.Pending:
                    return "N/A";
                case Status.Shipped:
                    return estimatedDeliveryDate.ToString();
                case Status.Acquired:
                case Status.Delivered:
                    return "Delivered";
                default: throw new InvalidOperationException();
            }
        }
    }
}
