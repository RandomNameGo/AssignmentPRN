using Repositories;
using Repositories.Entities;

namespace Services
{
    public class UserAccountService
    {
        private UserAccountRepository repository = new();

        public UserAccount Authenticate(string email, string password)
        {
            return repository.GetOne(email, password);
        }
    }
}
