using Application.Interfaces.IOrder;
using Application.Interfaces.IReceipt;

namespace Application.Interfaces.IUnitOfWork
{
    public interface IUnitOfWorkFinishedOrder : IDisposable
    {
        IOrderCommand OrderCommand { get; }
        IReceiptCommand ReceiptCommand { get; }
        int Save();
    }
}
