using Application.Common.Models;
using Application.UseCase.V2.User.GetAll;

namespace Application.Interfaces.IUser
{
    public interface IGetUsers
    {
        Result<PaginatedListResponse<UserResponse>> GetAllUsers(int index, int quantity);
    }
}
