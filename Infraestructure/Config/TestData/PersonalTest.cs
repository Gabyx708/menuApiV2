using Application.Tools.Encrypt;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config.TestData
{
    public class PersonalTest : IEntityTypeConfiguration<Personal>
    {
        public void Configure(EntityTypeBuilder<Personal> builder)
        {
            builder.HasData
                (
                    new Personal
                    {
                        IdPersonal = Guid.NewGuid(),
                        Nombre = "Juan",
                        Apellido = "Pérez",
                        Dni = "123456789",
                        FechaNac = new DateTime(1990, 5, 15),
                        FechaIngreso = new DateTime(2022, 1, 1),
                        FechaAlta = new DateTime(2022, 1, 1),
                        Mail = "juanperez@example.com",
                        Telefono = "1234567890",
                        Privilegio = 2,
                        Password = Encrypt256.GetSHA256("12345")
                    },
                    new Personal
                    {
                        IdPersonal = Guid.NewGuid(),
                        Nombre = "María",
                        Apellido = "González",
                        Dni = "987654321",
                        FechaNac = new DateTime(1988, 10, 20),
                        FechaIngreso = new DateTime(2021, 3, 10),
                        FechaAlta = new DateTime(2021, 3, 10),
                        Mail = "mariagonzalez@example.com",
                        Telefono = "0987654321",
                        Privilegio = 2,
                        Password = Encrypt256.GetSHA256("12345")
                    },
                    new Personal
                    {
                        IdPersonal = Guid.NewGuid(),
                        Nombre = "Carlos",
                        Apellido = "López",
                        Dni = "456789123",
                        FechaNac = new DateTime(1995, 8, 25),
                        FechaIngreso = new DateTime(2023, 2, 5),
                        FechaAlta = new DateTime(2023, 2, 5),
                        Mail = "carloslopez@example.com",
                        Telefono = "678912345",
                        Privilegio = 2,
                        Password = Encrypt256.GetSHA256("12345")
                    },
                    new Personal
                    {
                        IdPersonal = Guid.NewGuid(),
                        Nombre = "Ana",
                        Apellido = "Ramírez",
                        Dni = "321654987",
                        FechaNac = new DateTime(1992, 4, 12),
                        FechaIngreso = new DateTime(2020, 7, 20),
                        FechaAlta = new DateTime(2020, 7, 20),
                        Mail = "anaramirez@example.com",
                        Telefono = "543216549",
                        Privilegio = 2,
                        Password = Encrypt256.GetSHA256("12345")
                    },
                    new Personal
                    {
                        IdPersonal = Guid.NewGuid(),
                        Nombre = "Pedro",
                        Apellido = "Martínez",
                        Dni = "789456123",
                        FechaNac = new DateTime(1985, 12, 8),
                        FechaIngreso = new DateTime(2021, 9, 15),
                        FechaAlta = new DateTime(2021, 9, 15),
                        Mail = "pedromartinez@example.com",
                        Telefono = "987654321",
                        Privilegio = 2,
                        Password = Encrypt256.GetSHA256("12345")
                    },
                    new Personal
                    {
                        IdPersonal = Guid.NewGuid(),
                        Nombre = "Laura",
                        Apellido = "Hernández",
                        Dni = "654123987",
                        FechaNac = new DateTime(1991, 7, 4),
                        FechaIngreso = new DateTime(2022, 6, 1),
                        FechaAlta = new DateTime(2022, 6, 1),
                        Mail = "laurahernandez@example.com",
                        Telefono = "876543219",
                        Privilegio = 2,
                        Password = Encrypt256.GetSHA256("12345")
                    },
                     new Personal
                     {
                         IdPersonal = Guid.NewGuid(),
                         Nombre = "Diego",
                         Apellido = "Fernández",
                         Dni = "258963147",
                         FechaNac = new DateTime(1987, 3, 18),
                         FechaIngreso = new DateTime(2023, 4, 10),
                         FechaAlta = new DateTime(2023, 4, 10),
                         Mail = "diegofernandez@example.com",
                         Telefono = "741852963",
                         Privilegio = 2,
                         Password = Encrypt256.GetSHA256("12345")
                     },
                     new Personal
                     {
                         IdPersonal = Guid.NewGuid(),
                         Nombre = "Sofía",
                         Apellido = "López",
                         Dni = "741852963",
                         FechaNac = new DateTime(1993, 2, 28),
                         FechaIngreso = new DateTime(2020, 12, 5),
                         FechaAlta = new DateTime(2020, 12, 5),
                         Mail = "sofialopez@example.com",
                         Telefono = "369258147",
                         Privilegio = 2,
                         Password = Encrypt256.GetSHA256("12345")
                     },
                     new Personal
                     {
                         IdPersonal = Guid.NewGuid(),
                         Nombre = "Javier",
                         Apellido = "Gómez",
                         Dni = "963852741",
                         FechaNac = new DateTime(1986, 9, 9),
                         FechaIngreso = new DateTime(2023, 1, 20),
                         FechaAlta = new DateTime(2023, 1, 20),
                         Mail = "javiergomez@example.com",
                         Telefono = "159357852",
                         Privilegio = 2,
                         Password = Encrypt256.GetSHA256("12345")
                     },
                     new Personal
                     {
                         IdPersonal = Guid.NewGuid(),
                         Nombre = "Isabella",
                         Apellido = "Díaz",
                         Dni = "159357852",
                         FechaNac = new DateTime(1994, 11, 30),
                         FechaIngreso = new DateTime(2022, 10, 10),
                         FechaAlta = new DateTime(2022, 10, 10),
                         Mail = "isabelladiaz@example.com",
                         Telefono = "852963741",
                         Privilegio = 2,
                         Password = Encrypt256.GetSHA256("12345")
                     }


                );
        }
    }
}
