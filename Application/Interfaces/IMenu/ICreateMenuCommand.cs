using Application.UseCase.V2.Menu.Create;

namespace Application.Interfaces.IMenu
{
    public interface ICreateMenuCommand
    {
        public Result<CreateMenuResponse> CreateMenu(CreateMenuRequest request);
    }
}
