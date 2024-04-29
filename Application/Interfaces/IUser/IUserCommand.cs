using Domain.Entities;

namespace Application.Interfaces.IUser
{
    public interface IUserCommand
    {
        User InsertUser(User user);
        User UpdateUser(User user);
    }
}
