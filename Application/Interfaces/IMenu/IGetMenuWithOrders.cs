using Application.UseCase.V2.Menu.GetWithOrders;

namespace Application.Interfaces.IMenu
{
    public interface IGetMenuWithOrders
    {
        Result<MenuWithOrdersResponse> GetMenuWithAllOrders(Guid idMenu);
    }
}
