using Application.Interfaces.IOrder;
using Domain.Dtos;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
            return _context.Orders
                            .Include(o => o.Receipt)
                            .ThenInclude(r => r.Discount)
                            .Include(o => o.Transitions)
                            .ThenInclude(t => t.InitialState)
                            .Include(o => o.Transitions)
                            .ThenInclude(t => t.FinalSate)
                            .Include(o => o.User)
                            .Include(o => o.State)
                            .Include(o => o.Items)
                            .ThenInclude(item => item.MenuOption)
                            .ThenInclude(moption => moption.Dish)
                            .FirstOrDefault(o => o.IdOrder == id)
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
            var ordersUser = _context.Orders.Where(o => o.IdUser == idUser)
                                            .OrderByDescending(o => o.OrderDate);

            return PaginatedList<Order>.Create(ordersUser, index, quantity);
        }

        public List<Order> GetOrdersByMenu(Guid idMenu)
        {
            var orders = _context.Orders
                    .Include(order => order.State)
                    .Include(order => order.User) 
                    .Include(order => order.Items) 
                        .ThenInclude(item => item.MenuOption) 
                            .ThenInclude(option => option.Dish) 
                    .Where(order => order.Items.Any(item => item.MenuOption.Menu.IdMenu == idMenu)) 
                    .ToList();


            return orders;
        }

    }
}
