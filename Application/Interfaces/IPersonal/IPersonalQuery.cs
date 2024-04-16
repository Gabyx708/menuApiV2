using Domain.Entities;

namespace Application.Interfaces.IPersonal
{
    public interface IPersonalQuery
    {
        User GetPersonalById(Guid idPersonal);
        List<User> GetAll();
    }
}
