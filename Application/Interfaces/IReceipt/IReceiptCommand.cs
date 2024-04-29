using Domain.Entities;

namespace Application.Interfaces.IReceipt
{
    public interface IReceiptCommand
    {
        Receipt InsertReceipt(Receipt receipt);
    }
}
