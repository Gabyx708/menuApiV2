//using Application.Tools.Encrypt;
//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infraestructure.TestData
//{
//    public class AdministratorTest : IEntityTypeConfiguration<User>
//    {
//        public void Configure(EntityTypeBuilder<User> builder)
//        {
//            builder.HasData(

//                new User
//                {
//                    IdUser = "12345679",
//                    Name = "Administrador",
//                    LastName = "Aker",
//                    BirthDate = new DateTime(1990, 5, 15),
//                    RegistrationDate = new DateTime(2022, 1, 1),
//                    Privilege = 1,
//                    Password = Encrypt256.GetSHA256("1234")
//                },

//                new User
//                {
//                    IdUser = "999999999",
//                    Name = "BOT",
//                    LastName = "BOT",
//                    BirthDate = new DateTime(2000, 1, 1),
//                    RegistrationDate = new DateTime(2022, 1, 1),
//                    Privilege = 1,
//                    Password = Encrypt256.GetSHA256("secret")
//                }
//            );
//        }


//    }
//}
