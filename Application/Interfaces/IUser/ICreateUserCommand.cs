using Application.UseCase.V2.User.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IUser
{
    public interface ICreateUserCommand
    {
        Result<CreateUserResponse> CreateNewMenuUser(CreateUserRequest request);
    }
}
