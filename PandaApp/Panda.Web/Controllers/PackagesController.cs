using Panda.Models;
using Panda.Models.Enums;
using Panda.Web.ViewModels.Packages;
using Panda.Web.ViewModels.Users;
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
        public IHttpResponse Details(string id)
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
        public IHttpResponse Create()
        {
            var recipients = this.Db.Users
                .Select(x => new UsernameViewModel { Username = x.Username });

            var model = new AllUsersViewModel { Recipients = recipients };

            return this.View(model);
        }

        [Authorize("Admin")]
        [HttpPost]
        public IHttpResponse Create(PackageViewModel model)
        {
            User recipient = this.Db.Users.FirstOrDefault(x => x.Username == model.Recipient);
            if (recipient == null)
                return this.BadRequestError("Recipient not found.");

            var package = new Package
            {
                Recipient = recipient,
                RecipientId = recipient.Id,
                Description = model.Description,
                EstimatedDeliveryDate = model.EstimatedDeliveryDate,
                ShippingAddress = model.ShippingAddress,
                Status = model.Status,
                Weight = model.Weight
            };

            this.Db.Packages.Add(package);
            this.Db.SaveChanges();

            return this.Redirect("/");
        }

        [Authorize("Admin")]
        public IHttpResponse Pending()
        {
            var pendingPackages = this.Db.Packages.Select(x =>
            new PackageViewModel
            {
                Id = x.Id,
                Weight = x.Weight,
                Status = x.Status,
                ShippingAddress = x.ShippingAddress,
                Description = x.Description,
                Recipient = x.Recipient.Username
            }).Where(x => x.Status == Status.Pending).ToList();

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
