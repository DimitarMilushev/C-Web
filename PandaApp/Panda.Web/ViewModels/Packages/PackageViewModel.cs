using Panda.Models.Enums;
using System;

namespace Panda.Web.ViewModels.Packages
{
    public class PackageViewModel
    {
        public string Id { get; set; }

        public string ShippingAddress { get; set; }

        public Status Status { get; set; } = Status.Pending;

        public double Weight { get; set; }

        public string Recipient { get; set; }

        public string Description { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; } = null;
    }
}
