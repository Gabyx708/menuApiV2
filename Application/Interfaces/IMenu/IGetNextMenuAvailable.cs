using Application.UseCase.V2.Menu.GetNextAvailable;

namespace Application.Interfaces.IMenu
{
    public interface IGetNextMenuAvailable
    {
        Result<GetNextMenuResponse> GetNextMenuAvailableForUsers();
    }
}
