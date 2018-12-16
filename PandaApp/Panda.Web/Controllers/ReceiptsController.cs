using Panda.Web.ViewModels.Receipts;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panda.Web.Controllers
{
    public class ReceiptsController : BaseController
    {
        [Authorize]
        public IHttpResponse Index()
        {
            var receipts = this.Db.Receipts.Select(x =>
            new ReceiptViewModel
            {
                Id = x.Id,
                Fee = x.Fee,
                IssuedOn = x.IssuedOn,
                Recipient = x.Recipient.Username
            }).ToList();

           if(receipts.Count == 0)
                return this.BadRequestError("There are no receipts");


            var model = new AllReceiptsViewModel { Receipts = receipts };
            return this.View(model);
        }

        [Authorize("Admin")]
        public IHttpResponse Details(string id)
        {
            var viewModel = this.Db.Receipts
                .Select(x => new ReceiptDetailsViewModel
                {
                    Id = x.Id,
                    IssuedOn = x.IssuedOn.ToShortDateString(),
                    Address = x.Package.ShippingAddress,
                    Weight = x.Package.Weight,
                    Recipient = x.Recipient.Username,
                    Description = x.Package.Description,
                    Fee = x.Fee
                })
                .FirstOrDefault(x => x.Id == id);

            if (viewModel == null)
            {
                return this.BadRequestError("Invalid product id.");
            }

            return this.View(viewModel);
        }
    }
}
