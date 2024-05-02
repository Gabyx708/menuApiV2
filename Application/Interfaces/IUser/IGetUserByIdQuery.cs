using Application.UseCase.V2.User.GetById;

namespace Application.Interfaces.IUser
{
    public interface IGetUserByIdQuery
    {
        Result<UserByIdResponse> GetUser(string id);
    }
}
