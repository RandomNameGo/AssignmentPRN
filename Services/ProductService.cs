using Repositories;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService
    {
        private ProductRepository _repo = new();
        public List<Product> GetAllProducts()
        {
            return _repo.GetProducts();
        }

        public void CreateProduct(Product product)
        {
            _repo.Create(product);
        }

        public void UpdateProduct(Product product)
        {
            _repo.Update(product);
        }
        public void DeleteProduct(Product product)
        {
            _repo.Delete(product);
        }
    }
}
