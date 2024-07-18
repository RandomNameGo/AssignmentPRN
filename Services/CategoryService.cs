using Repositories;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService
    {
        private CategoryRepository _repo = new();

        public List<Category> GetCategories()
        {
            return _repo.GetCategories();
        }
    }
}
