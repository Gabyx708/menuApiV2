using Domain.Entities;

namespace Application.Interfaces.IPersonal
{
    public interface IPersonalCommand
    {
        User createPersonal(User personal);
        User updatePersonal(Guid idPersonal, User personal);
    }
}
