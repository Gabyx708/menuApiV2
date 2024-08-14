using Application.Helpers.Encrypt;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.TestData
{
    public class AdministratorTest : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(

                new User
                {
                    IdUser = "admin",
                    Name = "admin",
                    NickName = "admin",
                    LastName = "not use",
                    BirthDate = new DateTime(1800, 5, 15),
                    RegistrationDate = DateTime.Now,
                    Privilege = 1,
                    Password = Encrypt256.GetSHA256("admin")
                }
            );
        }


    }
}
