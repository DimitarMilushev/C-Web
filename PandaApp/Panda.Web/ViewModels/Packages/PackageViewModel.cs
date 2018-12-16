using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Web.ViewModels.Packages
{
    public class PackageViewModel
    {
        public string Id { get; set; }

        public string ShippingAddress { get; set; }

        public string Status { get; set; }

        public double Weight { get; set; }

        public string Recipient { get; set; }

        public string Description { get; set; }
    }
}
