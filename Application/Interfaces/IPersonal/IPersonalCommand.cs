using Domain.Entities;

namespace Application.Interfaces.IPersonal
{
    public interface IPersonalCommand
    {
        Personal createPersonal(Personal personal);
        Personal updatePersonal(Guid idPersonal, Personal personal);
    }
}
