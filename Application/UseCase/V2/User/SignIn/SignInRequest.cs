namespace Application.UseCase.V2.User.SignIn
{
    public class SignInRequest
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
