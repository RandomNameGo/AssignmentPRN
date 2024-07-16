using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserAccountRepository
    {
        private InvenGameDbContext _context;

        public UserAccount GetOne(string email, string password)
        {
            _context = new InvenGameDbContext();
            return _context.UserAccounts.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
