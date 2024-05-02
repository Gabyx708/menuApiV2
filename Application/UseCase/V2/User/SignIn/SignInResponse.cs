namespace Application.UseCase.V2.User.SignIn
{
    public class SignInResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NickName { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
