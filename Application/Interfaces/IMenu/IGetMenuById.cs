using Application.UseCase.V2.Menu.GetById;

namespace Application.Interfaces.IMenu
{
    public interface IGetMenuById
    {
        Result<GetMenuByIdResponse> GetMenuByIdResponse(Guid idMenu);
    }
}
