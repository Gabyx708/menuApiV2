using Application.UseCase.V2.Menu.GetUserOrderFromMenu;

namespace Application.Interfaces.IMenu
{
    public interface IGetUserOrdersFromMenu
    {
        Result<List<UserOrderFromMenu>> GetUserOrdersFromMenu(string idMenu, string idUser);
    }
}
