using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Web.ViewModels.Receipts
{
    public class AllReceiptsViewModel
    {
        public IEnumerable<ReceiptViewModel> Receipts { get; set; }
    }
}
