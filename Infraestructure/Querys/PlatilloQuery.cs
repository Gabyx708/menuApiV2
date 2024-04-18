//using Application.Interfaces.IPlatillo;
//using Domain.Entities;
//using Infraestructure.Persistence;

//namespace Infraestructure.Querys
//{
//    public class PlatilloQuery : IPlatilloQuery
//    {
//        private readonly MenuAppContext _context;

//        public PlatilloQuery(MenuAppContext context)
//        {
//            _context = context;
//        }

//        public Dish GetPlatilloById(int id)
//        {
//            var platilloEncontrado = _context.Platillos.Single(p => p.IdPlatillo == id);

//            if (platilloEncontrado != null) { return platilloEncontrado; }

//            return null;
//        }

//        public List<Dish> GetAll()
//        {
//            return _context.Platillos.ToList();
//        }

//    }
//}
