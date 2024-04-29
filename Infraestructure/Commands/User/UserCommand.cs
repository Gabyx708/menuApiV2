using Application.Interfaces.IUser;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class UserCommand : IUserCommand
    {
        private readonly MenuAppContext _context;

        public UserCommand(MenuAppContext context)
        {
            _context = context;
        }

        public User InsertUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.IdUser == user.IdUser);

            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.LastName = user.LastName;
                existingUser.NickName  = user.NickName;
                existingUser.Privilege = user.Privilege;
                existingUser.Password = user.Password;

                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("User not found");
            }

            return existingUser;
        }
    }
}
