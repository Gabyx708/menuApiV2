using Application.UseCase.V2.Menu.GetById;

namespace Application.Interfaces.IMenu
{
    public interface IGetMenuByIdQuery
    {
        Result<GetMenuByIdResponse> GetMenuByIdResponse(string idMenu);
    }
}
