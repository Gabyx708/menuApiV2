//using Application.Interfaces.IDescuento;
//using Domain.Entities;
//using Infraestructure.Persistence;

//namespace Infraestructure.Querys
//{
//    public class DescuentoQuery : IDescuentoQuery
//    {
//        private readonly MenuAppContext _context;

//        public DescuentoQuery(MenuAppContext context)
//        {
//            _context = context;
//        }

//        public List<Discount> GetAll()
//        {
//            return _context.Discount.ToList();
//        }

//        public Discount GetByFecha(DateTime fecha)
//        {
//            var descuento = _context.Discounts.Single(d => d.FechaInicioVigencia == fecha);
//            return descuento;
//        }

//        public Discount GetById(Guid idDescuento)
//        {
//            var descuento = _context.Discount.FirstOrDefault(d => d.IdDescuento == idDescuento);
//            return descuento;
//        }

//        public Discount GetVigente()
//        {
//            var vigente = _context.Discount.OrderByDescending(d => d.FechaInicioVigencia).FirstOrDefault();
//            return vigente;
//        }
//    }
//}
