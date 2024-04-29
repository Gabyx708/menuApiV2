using Application.Interfaces.IOrder;
using Domain.Entities;
using Domain.Enums;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
            var foundOrder = _context.Orders
                                      .Include(o => o.Items)
                                      .FirstOrDefault(O => O.IdOrder ==idOrder);

            if (foundOrder == null)
            {
                throw new NullReferenceException();
            }

            if (foundOrder.StateCode == (int)OrderState.Finished || 
                foundOrder.StateCode == (int)OrderState.Cancelled)
            {
                throw new InvalidOperationException();
            }


            var transition = new Transition
            {
                IdOrder = foundOrder.IdOrder,
                InitialStateCode = foundOrder.StateCode,
                FinalStateCode =(int)OrderState.Cancelled,
                Date = DateTime.Now
            };

            _context.Transitions.Add(transition);
            foundOrder.StateCode = (int)OrderState.Cancelled;

            foreach (var item in foundOrder.Items)
            {
                var menuOp = _context.MenuOptions.Find(item.IdMenu, item.IdDish);

                menuOp!.Requested = (menuOp.Requested - item.Quantity);

                _context.Update(menuOp);
            }

            _context.Orders.Update(foundOrder);

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

            if (foundOrder.StateCode == (int)OrderState.Finished 
             || foundOrder.StateCode == (int)OrderState.Cancelled)
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
