using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription {
            get
            {
                if (this.Description.Length > 50)
                    return this.Description.Substring(0, 50) + "...";
                else
                    return this.Description;
            }
        }

        public decimal Price { get; set; }
    }
}
