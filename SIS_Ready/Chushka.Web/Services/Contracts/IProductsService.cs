using Chushka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chushka.Web.Services.Contracts
{
    public interface IProductsService
    {
        IQueryable<Product> All();

        Product GetProductById(int id);

        void CreateProduct(Product product);

        void EditProduct(int id);
    }
}
