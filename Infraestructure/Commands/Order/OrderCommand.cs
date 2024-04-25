using Application.Interfaces.IOrder;
using Domain.Entities;
using Domain.Enums;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class OrderCommand : IOrderCommand
    {
        private readonly MenuAppContext _context;

        public OrderCommand(MenuAppContext context)
        {
            _context = context;
        }

        public Order CancelOrder(Guid idOrder)
        {
            var foundOrder = _context.Orders.Find(idOrder);

            if (foundOrder == null)
            {
                throw new NullReferenceException();
            }

            if (foundOrder.StateCode == (int)OrderState.Finished)
            {
                throw new InvalidOperationException();
            }

            var transition = new Transition
            {
                IdOrder = foundOrder.IdOrder,
                InitialStateCode = foundOrder.StateCode,
                FinalStateCode = (int)OrderState.Cancelled,
                Date = DateTime.Now
            };

            foundOrder.StateCode = (int)OrderState.Cancelled;

            _context.Orders.Update(foundOrder);
            _context.Transitions.Add(transition);

            _context.SaveChanges();
            return foundOrder;
        }

        public Order ChangeOrderState(Guid idOrder, int idState)
        {
            var foundOrder = _context.Orders.Find(idOrder);

            if (foundOrder == null)
            {
                throw new NullReferenceException();
            }

            if (foundOrder.StateCode == (int)OrderState.Finished)
            {
                throw new InvalidOperationException();
            }

            var transition = new Transition
            {
                IdOrder = foundOrder.IdOrder,
                InitialStateCode = foundOrder.StateCode,
                FinalStateCode = idState,
                Date = DateTime.Now
            };

            foundOrder.StateCode = idState;

            _context.Orders.Update(foundOrder);
            _context.Transitions.Add(transition);

            _context.SaveChanges();
            return foundOrder;
        }

        public Order InsertOrder(Order order)
        {
            _context.Add(order);
            _context.AddRange(order.Items);
           
            return order;
        }
    }
}
