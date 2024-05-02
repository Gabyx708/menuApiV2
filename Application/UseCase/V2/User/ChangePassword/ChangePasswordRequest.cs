namespace Application.UseCase.V2.User.ChangePassword
{
    public class ChangePasswordRequest
    {
        public string username { get; set; } = null!;
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
