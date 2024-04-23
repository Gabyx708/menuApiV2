using Application.Interfaces.IOrder;
using Domain.Dtos;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys
{
    public class OrderQuery : IOrderQuery
    {
        private readonly MenuAppContext _context;

        public OrderQuery(MenuAppContext context)
        {
            _context = context;
        }

        public PaginatedList<Order> GetAll(int index, int quantity)
        {
            var orders = from o in _context.Orders select o;

            return PaginatedList<Order>.Create(orders, index, quantity);
        }

        public Order GetOrderById(Guid id)
        {
            return _context.Orders.Find(id)
                    ?? throw new NullReferenceException();
        }

        public List<Order> GetOrdersByMenuAndUser(Guid idMenu, string idUser)
        {
            var orders = _context.Orders.Where(o => o.IdUser == idUser);

            var orderIdsWithIdMenu = _context.OrderItems.
                                   Where(oi => oi.IdMenu == idMenu)
                                   .Select(oi => oi.IdOrder);

            orders = orders.Where(o => orderIdsWithIdMenu.Contains(o.IdOrder));

            return orders.ToList();
        }

        public PaginatedList<Order> GetOrdersFromUser(string idUser, int index, int quantity)
        {
            var ordersUser = _context.Orders.Where(o => o.IdUser == idUser);

            return PaginatedList<Order>.Create(ordersUser, index, quantity);
        }
    }
}
