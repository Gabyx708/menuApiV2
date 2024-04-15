using Application.Interfaces.IAuthentication;
using Application.Tools.Encrypt;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys
{
    public class AutehenticationQuery : IAuthenticacionQuery
    {
        private readonly MenuAppContext _context;

        public AutehenticationQuery(MenuAppContext context)
        {
            _context = context;
        }

        public Personal Autenticarse(string dni, string password)
        {
            var contrasena = Encrypt256.GetSHA256(password);
            var personal = _context.Personales.FirstOrDefault(p => p.Dni == dni && p.Password == contrasena);
            return personal;
        }
    }
}
