using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SupplierRepository
    {
        private InvenGameDbContext _context;

        public List<Supplier> GetSuppliers()
        {
            _context = new InvenGameDbContext();
            return _context.Suppliers.ToList();
        }
    }
}
