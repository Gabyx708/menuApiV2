using Application.Interfaces.IUser;
using Domain.Dtos;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys
{
    public class UserQuery : IUserQuery
    {
        private readonly MenuAppContext _context;

        public UserQuery(MenuAppContext context)
        {
            _context = context;
        }

        public PaginatedList<User> GetAll(int index, int quantity)
        {
            var users = from u in _context.Users select u;

            users = users.OrderByDescending(u => u.Name);

            var page = PaginatedList<User>.Create(users, index, quantity);

            return page;
        }

        public User GetUserById(string id)
        {
            return _context.Users.Find(id)
                  ?? throw new NullReferenceException();
        }
    }
}
