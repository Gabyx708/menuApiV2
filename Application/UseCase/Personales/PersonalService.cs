using Application.Interfaces.IPersonal;
using Application.Request.PersonalRequests;
using Application.Response.PersonalResponses;
using Application.Tools.Encrypt;
using Domain.Entities;

namespace Application.UseCase.Personales
{
    public class PersonalService : IPersonalService
    {
        private readonly IPersonalCommand _command;
        private readonly IPersonalQuery _query;

        public PersonalService(IPersonalCommand command, IPersonalQuery query)
        {
            _command = command;
            _query = query;
        }

        public PersonalResponse createPersonal(PersonalRequest personalNuevo)
        {
            var nuevoPersonal = new Personal
            {
                Nombre = personalNuevo.nombre,
                Apellido = personalNuevo.apellido,
                Dni = personalNuevo.dni,
                FechaNac = personalNuevo.fecha_nacimiento,
                FechaAlta = DateTime.Now,
                FechaIngreso = personalNuevo.fecha_ingreso,
                Mail = personalNuevo.mail,
                Telefono = personalNuevo.telefono,
                Privilegio = personalNuevo.privilegio,
                Password = Encrypt256.GetSHA256(personalNuevo.dni)

            };

            _command.createPersonal(nuevoPersonal);
            var responseUsuario = GetPersonalById(nuevoPersonal.IdPersonal);
            return responseUsuario;
        }

        public List<PersonalResponse> GetAllPersonal()
        {
            var personalMapear = _query.GetAll();
            List<PersonalResponse> personales = new List<PersonalResponse>();

            foreach (var personal in personalMapear)
            {
                var personalAdd = new PersonalResponse
                {
                    id = personal.IdPersonal,
                    nombre = personal.Nombre,
                    apellido = personal.Apellido,
                    dni = personal.Dni,
                    fecha_nacimiento = personal.FechaNac,
                    fecha_ingreso = personal.FechaIngreso,
                    fecha_alta = personal.FechaAlta,
                    mail = personal.Mail,
                    telefono = personal.Telefono,
                    isAutomatico = personal.isAutomatico

                };

                personales.Add(personalAdd);
            }

            return personales;
        }

        public PersonalResponse GetPersonalById(Guid personalId)
        {
            var personalBuscado = _query.GetPersonalById(personalId);

            if (personalBuscado == null) { return null; };

            return new PersonalResponse
            {
                id = personalBuscado.IdPersonal,
                nombre = personalBuscado.Nombre,
                apellido = personalBuscado.Apellido,
                dni = personalBuscado.Dni,
                fecha_nacimiento = personalBuscado.FechaNac,
                fecha_alta = personalBuscado.FechaAlta,
                fecha_ingreso = personalBuscado.FechaIngreso,
                mail = personalBuscado.Mail,
                telefono = personalBuscado.Telefono,
                isAutomatico = personalBuscado.isAutomatico
            };
        }

        public PersonalResponse UpdatePersonal(Guid personalId, PersonalRequest personal)
        {

            var personalCambiado = new Personal
            {
                Nombre = personal.nombre,
                Apellido = personal.apellido,
                Dni = personal.dni,
                FechaNac = personal.fecha_nacimiento,
                FechaIngreso = personal.fecha_ingreso,
                Mail = personal.mail,
                Telefono = personal.telefono,
                Privilegio = personal.privilegio

            };

            _command.updatePersonal(personalId, personalCambiado);

            return GetPersonalById(personalId);
        }
    }
}
