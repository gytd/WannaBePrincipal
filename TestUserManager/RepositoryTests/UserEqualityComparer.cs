using System.Diagnostics.CodeAnalysis;
using WannaBePrincipal.Models;

namespace TestUserManager.RepositoryTests
{
    public class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User? x, User? y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }

            return x.Name == y.Name &&
                x.Email == y.Email &&
                x.Phone == y.Phone &&
                x.Website == y.Website;
        }

        public int GetHashCode([DisallowNull] User obj) => HashCode.Combine(obj.Name, obj.Email, obj.Address, obj.Company);
    }
}
