using Application.Interfaces.IMenuOption;
using Application.Interfaces.IOrder;

namespace Application.Interfaces.IUnitOfWork
{
    public interface IUnitOfWorkCreateOrder : IDisposable
    {
        IMenuOptionCommand MenuOptionCommand { get; }
        IOrderCommand OrderCommand { get; }
        int Save();
    }
}
