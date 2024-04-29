using Application.Common.Models;
using Application.Interfaces.IUser;

namespace Application.UseCase.V2.User.GetAll
{
    public class GetAllUsersQuery : IGetUsers
    {
        private readonly IUserQuery userQuery;

        public GetAllUsersQuery(IUserQuery userQuery)
        {
            this.userQuery = userQuery;
        }

        public Result<PaginatedListResponse<UserResponse>> GetAllUsers(int index, int quantity)
        {
            var usersPage = userQuery.GetAll(index, quantity);

            List<UserResponse> usersResponses = new();

            foreach (var user in usersPage.Items)
            {
                var userResponse = new UserResponse
                {
                    Id = user.IdUser,
                    Name = user.Name,
                    LastName = user.LastName,
                    NickName = user.NickName,
                    BirhtDay = user.BirthDate
                };

                usersResponses.Add(userResponse);
            }

            var page = new PaginatedListResponse<UserResponse>
            {
                Index = usersPage.PageIndex,
                TotalPages = usersPage.TotalPages,
                TotalRecords = usersPage.TotalRecords,
                Items = usersResponses
            };

            return Result<PaginatedListResponse<UserResponse>>.SuccessResult(page);
        }
    }
}
