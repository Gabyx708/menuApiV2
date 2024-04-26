using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces.IOrder
{
    public interface IOrderQuery
    {
        Order GetOrderById(Guid id);
        PaginatedList<Order> GetOrdersFromUser(string idUser, int index, int quantity);
        PaginatedList<Order> GetAll(int index,int quantity);
        List<Order> GetOrdersByMenuAndUser(Guid idMenu,string idUser);
        List<Order> GetOrdersByMenu(Guid idMenu);
    }
}
