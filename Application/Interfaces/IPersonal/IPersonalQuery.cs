using Domain.Entities;

namespace Application.Interfaces.IPersonal
{
    public interface IPersonalQuery
    {
        Personal GetPersonalById(Guid idPersonal);
        List<Personal> GetAll();
    }
}
