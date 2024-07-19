using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository
    {
        private InvenGameDbContext _context;

        public List<Category> GetCategories()
        {
            _context = new InvenGameDbContext();
            return _context.Categories.ToList();
        }
    }
}
