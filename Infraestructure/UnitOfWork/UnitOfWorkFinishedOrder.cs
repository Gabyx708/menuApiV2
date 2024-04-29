using Application.Interfaces.IOrder;
using Application.Interfaces.IReceipt;
using Application.Interfaces.IUnitOfWork;
using Infraestructure.Persistence;

namespace Infraestructure.UnitOfWork
{
    public class UnitOfWorkFinishedOrder : IUnitOfWorkFinishedOrder
    {
        public IOrderCommand OrderCommand { get; }
        public IReceiptCommand ReceiptCommand { get; }
        private readonly MenuAppContext _context;

        public UnitOfWorkFinishedOrder(IOrderCommand orderCommand, IReceiptCommand receiptCommand, MenuAppContext context)
        {
            OrderCommand = orderCommand;
            ReceiptCommand = receiptCommand;
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
