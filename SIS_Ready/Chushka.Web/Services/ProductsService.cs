using Chushka.Data;
using Chushka.Models;
using Chushka.Web.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chushka.Web.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ChushkaContext context;

        public ProductsService(ChushkaContext context)
        {
            this.context = context;
        }

        public IQueryable<Product> All()
            => this.context.Products;

        public void CreateProduct(Product product)
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
        }

        public void EditProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        => this.context.Products.FirstOrDefault(x => x.Id == id);
    }
}
