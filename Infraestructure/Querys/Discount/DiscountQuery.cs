using Application.Interfaces.IDiscount;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys
{
    public class DiscountQuery : IDiscountQuery
    {
        private readonly MenuAppContext _context;

        public DiscountQuery(MenuAppContext context)
        {
            _context = context;
        }

        public Discount GetLastestDiscount()
        {
            return _context.Discounts.OrderByDescending(d => d.StartDate)
                                     .FirstOrDefault()
                                     ?? throw new NullReferenceException();
        }
    }
}
