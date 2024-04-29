namespace Application.UseCase.V2.User.GetAll
{
    public class GetAllUserResponse
    {
        public List<UserResponse> Users { get; set; } = null!;
    }

    public class UserResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NickName { get; set; } = null!;
        public DateTime BirhtDay { get; set; }
    }
}
