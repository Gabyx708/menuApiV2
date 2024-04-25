using Application.Interfaces.IMenuOption;
using Application.Interfaces.IOrder;
using Application.Interfaces.IUnitOfWork;
using Infraestructure.Persistence;

namespace Infraestructure.UnitOfWork
{
    public class UnitOfWorkCreateOrder : IUnitOfWorkCreateOrder
    {
        public IOrderCommand OrderCommand { get; }
        public IMenuOptionCommand MenuOptionCommand { get; }
        private readonly MenuAppContext _context;
        public UnitOfWorkCreateOrder(IMenuOptionCommand menuOptionCommand,
                                     IOrderCommand orderCommand,
                                    MenuAppContext context)
        {
            MenuOptionCommand = menuOptionCommand;
            OrderCommand = orderCommand;
            _context = context;
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        => _context.SaveChanges();
    }
}
