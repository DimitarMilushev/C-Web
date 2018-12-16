using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Web.ViewModels.Receipts
{
    public class ReceiptDetailsViewModel
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public string IssuedOn { get; set; }

        public decimal Fee { get; set; }

        public double Weight { get; set; }

        public string Recipient { get; set; }

        public string Description { get; set; }
    }
}
