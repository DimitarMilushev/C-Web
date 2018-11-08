using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Web.ViewModels
{
    public class ProductViewModelWrapper
    {
        public ProductViewModelWrapper()
        {
            ProductViewModels = new List<ProductViewModel>();
        }

        public ICollection<ProductViewModel> ProductViewModels { get; set; }
    }
}
