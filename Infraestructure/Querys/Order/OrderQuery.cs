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
            var ordersUser = _context.Orders.Where(o => o.IdUser == idUser);

            return PaginatedList<Order>.Create(ordersUser, index, quantity);
        }

        public List<Order> GetOrdersByMenu(Guid idMenu)
        {
            var orders = _context.Orders
                    .Include(order => order.State)
                    .Include(order => order.User) // Incluye el usuario asociado a cada orden
                    .Include(order => order.Items) // Incluye los ítems de cada orden
                        .ThenInclude(item => item.MenuOption) // Incluye la opción de menú asociada a cada ítem
                            .ThenInclude(option => option.Dish) // Incluye el plato asociado a cada opción de menú
                    .Where(order => order.Items.Any(item => item.MenuOption.Menu.IdMenu == idMenu)) // Filtra las órdenes relacionadas con el menú específico
                    .ToList();


            return orders;
        }

    }
}
