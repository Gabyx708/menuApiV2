namespace Application.UseCase.V2.User.Create
{
    public class CreateUserRequest
    {
        public string IdUser { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Privilege { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
