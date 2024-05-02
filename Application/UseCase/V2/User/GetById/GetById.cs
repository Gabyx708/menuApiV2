using Application.Interfaces.IUser;
using Microsoft.IdentityModel.Tokens;

namespace Application.UseCase.V2.User.GetById
{
    public class GetById : IGetUserByIdQuery
    {
        private readonly IUserQuery userQuery;

        public GetById(IUserQuery userQuery)
        {
            this.userQuery = userQuery;
        }

        public Result<UserByIdResponse> GetUser(string id)
        {
            if (id.IsNullOrEmpty() || id.Length < 3)
            {
                return Result<UserByIdResponse>.ValidationResult($"The user ID format is incorrect");
            }

            Domain.Entities.User user;

            try
            {
                user = userQuery.GetUserById(id);
            }catch(NullReferenceException)
            {
                return Result<UserByIdResponse>.NotFoundResult($"The user with ID: {id} does not exist");
            }

            var userResponse = new UserByIdResponse
            {
                Id = user.IdUser,
                Name = user.Name,
                LastName = user.LastName,
                Nickname = user.NickName,
                BirhtDate = user.BirthDate,
                RegistrationDate = user.RegistrationDate,
            };

            return Result<UserByIdResponse>.SuccessResult(userResponse);
        }
    }
}
