namespace Domain.Entities
{
    public class User
    {
        public string IdUser { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NickName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Privilege { get; set; }
        public string Password { get; set; } = null!;

    }
}
