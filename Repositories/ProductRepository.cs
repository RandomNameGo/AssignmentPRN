using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository
    {
        private InvenGameDbContext _context;


        public List<Product> GetProducts()
        {
            _context = new InvenGameDbContext();
             return _context.Products.Include("Category").Include("Supplier").ToList();
        }

        public void Create(Product product)
        {
            _context = new InvenGameDbContext();
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context = new InvenGameDbContext();
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context = new InvenGameDbContext();
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public Product GetByName(string name)
        {
            return _context.Products.FirstOrDefault(p => p.ProductName == name);
        }
    }
}
