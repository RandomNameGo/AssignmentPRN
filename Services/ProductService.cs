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
        public void DeleteProduct(Product product)
        {
            _repo.Delete(product);
        }
        public List<Product> SearchProduct(String productName, int? supplierId, int? maxPrice, int? minPrice, Boolean checkIsInStock)
        {
            List<Product> productList = _repo.GetProducts();
            productList = productList.Where(x=>x.ProductName.Contains(productName)).ToList();
            if(supplierId != null)
            {
                productList = productList.Where(x => x.SupplierId.Equals(supplierId)).ToList();
            }
            if(maxPrice != null)
            {
                productList = productList.Where(x => x.Price <= maxPrice).ToList();
            }
            if(minPrice != null)
            {
                productList = productList.Where(x => x.Price >= minPrice).ToList();
            }
            if (checkIsInStock)
            {
                productList = productList.Where(x => x.Quantity>0).ToList();
            }
            return productList;
        }
    }
}
