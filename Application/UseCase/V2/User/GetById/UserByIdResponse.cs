namespace Application.UseCase.V2.User.GetById
{
    public class UserByIdResponse
    {
        public string Id { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirhtDate { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
