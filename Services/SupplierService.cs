using Repositories;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SupplierService
    {
        private SupplierRepository _repo = new();

        public List<Supplier> GetSuppliers()
        {
            return _repo.GetSuppliers();
        }
    }
}
