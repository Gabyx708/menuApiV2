using Application.Request.MenuRequests;
using Application.Response.MenuResponses;

namespace Application.Interfaces.IMenu
{
    public interface IMenuService
    {
        MenuResponse CreateMenu(MenuRequest request);
        MenuResponse GetMenuById(Guid id);

        MenuResponse GetUltimoMenu();
        MenuResponse BorrarMenu(Guid idMenu);
    }
}
