//using Application.Tools.Encrypt;
//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infraestructure.TestData
//{
//    public class UserTest : IEntityTypeConfiguration<User>
//    {
//        public void Configure(EntityTypeBuilder<User> builder)
//        {
//            builder.HasData
//                (
//                    new User
//                    {
//                        IdUser = "123456789",
//                        Name = "Juan",
//                        LastName = "Pérez",
//                        BirthDate = new DateTime(1990, 5, 15),
//                        RegistrationDate = new DateTime(2022, 1, 1),
//                        Privilege = 2,
//                        Password = Encrypt256.GetSHA256("12345")
//                    },
//                    new User
//                    {
//                        IdUser = "987654321",
//                        Name = "María",
//                        LastName = "González",
//                        BirthDate = new DateTime(1988, 10, 20),
//                        RegistrationDate = new DateTime(2021, 3, 10),
//                        Privilege = 2,
//                        Password = Encrypt256.GetSHA256("12345")
//                    },
//                    new User
//                    {
//                        IdUser = "456789123",
//                        Name = "Carlos",
//                        LastName = "López",
//                        BirthDate = new DateTime(1995, 8, 25),
//                        RegistrationDate = new DateTime(2023, 2, 5),
//                        Privilege = 2,
//                        Password = Encrypt256.GetSHA256("12345")
//                    },
//                    new User
//                    {
//                        IdUser = "321654987",
//                        Name = "Ana",
//                        LastName = "Ramírez",
//                        BirthDate = new DateTime(1992, 4, 12),
//                        RegistrationDate = new DateTime(2020, 7, 20),
//                        Privilege = 2,
//                        Password = Encrypt256.GetSHA256("12345")
//                    },
//                    new User
//                    {
//                        IdUser = "789456123",
//                        Name = "Pedro",
//                        LastName = "Martínez",
//                        BirthDate = new DateTime(1985, 12, 8),
//                        RegistrationDate = new DateTime(2021, 9, 15),
//                        Privilege = 2,
//                        Password = Encrypt256.GetSHA256("12345")
//                    },
//                    new User
//                    {
//                        IdUser = "654123987",
//                        Name = "Laura",
//                        LastName = "Hernández",
//                        BirthDate = new DateTime(1991, 7, 4),
//                        RegistrationDate = new DateTime(2022, 6, 1),
//                        Privilege = 2,
//                        Password = Encrypt256.GetSHA256("12345")
//                    },
//                     new User
//                     {
//                         IdUser = "258963147",
//                         Name = "Diego",
//                         LastName = "Fernández",
//                         BirthDate = new DateTime(1987, 3, 18),
//                         RegistrationDate = new DateTime(2023, 4, 10),
//                         Privilege = 2,
//                         Password = Encrypt256.GetSHA256("12345")
//                     },
//                     new User
//                     {
//                         IdUser = "741852963",
//                         Name = "Sofía",
//                         LastName = "López",
//                         BirthDate = new DateTime(1993, 2, 28),
//                         RegistrationDate = new DateTime(2020, 12, 5),  
//                         Privilege = 2,
//                         Password = Encrypt256.GetSHA256("12345")
//                     },
//                     new User
//                     {
//                         IdUser = "963852741",
//                         Name = "Javier",
//                         LastName = "Gómez",
//                         BirthDate = new DateTime(1986, 9, 9),
//                         RegistrationDate = new DateTime(2023, 1, 20),
//                         Privilege = 2,
//                         Password = Encrypt256.GetSHA256("12345")
//                     },
//                     new User
//                     {
//                         IdUser = "159357852",
//                         Name = "Isabella",
//                         LastName = "Díaz",
//                         BirthDate = new DateTime(1994, 11, 30),
//                         RegistrationDate = new DateTime(2022, 10, 10),
//                         Privilege = 2,
//                         Password = Encrypt256.GetSHA256("12345")
//                     }


//                );
//        }
//    }
//}
