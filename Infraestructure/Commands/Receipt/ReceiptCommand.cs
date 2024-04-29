using Application.Interfaces.IReceipt;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class ReceiptCommand : IReceiptCommand
    {
        private readonly MenuAppContext _context;

        public ReceiptCommand(MenuAppContext context)
        {
            _context = context;
        }

        public Receipt InsertReceipt(Receipt receipt)
        {
            _context.Receipts.Add(receipt);
            return receipt;
        }
    }
}
