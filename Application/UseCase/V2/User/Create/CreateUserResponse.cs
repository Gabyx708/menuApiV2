namespace Application.UseCase.V2.User.Create
{
    public class CreateUserResponse : CreateUserRequest
    {
        public DateTime RegistrationDate { get; set; }
    }
}
