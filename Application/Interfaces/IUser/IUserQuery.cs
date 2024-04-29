using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces.IUser
{
    public interface IUserQuery
    {
        User GetUserById(string id);
        PaginatedList<User> GetAll(int index, int quantity);
    }
}
