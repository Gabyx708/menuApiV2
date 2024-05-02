using Application.Interfaces.IAuthentication;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys.Authentication
{
    public class AuthenticacionQuery : IAuthenticationQuery
    {
        private readonly MenuAppContext _context;

        public AuthenticacionQuery(MenuAppContext context)
        {
            _context = context;
        }

        public User AuthenticateUser(string username, string password)
        {
            var user = _context.Users.Where(u => u.IdUser == username
                                            && u.Password == password)
                                            .FirstOrDefault();
            if (user == null)
            {
                throw new NullReferenceException();
            }

            return user;
        }
    }
}
