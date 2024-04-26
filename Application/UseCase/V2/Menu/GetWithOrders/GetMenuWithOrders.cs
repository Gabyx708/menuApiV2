using Application.Interfaces.IMenu;
using Application.Interfaces.IOrder;
using Domain.Enums;

namespace Application.UseCase.V2.Menu.GetWithOrders
{
    public class GetMenuWithOrders : IGetMenuWithOrders
    {
        private readonly IMenuQuery menuQuery;
        private readonly IOrderQuery orderQuery;
        public GetMenuWithOrders(IMenuQuery menuQuery, IOrderQuery orderQuery)
        {
            this.menuQuery = menuQuery;
            this.orderQuery = orderQuery;
        }

        public Result<MenuWithOrdersResponse> GetMenuWithAllOrders(Guid idMenu)
        {
            Domain.Entities.Menu menu;
            try
            {
                menu = menuQuery.GetMenuById(idMenu);
            }
            catch (ArgumentNullException)
            {
                return Result<MenuWithOrdersResponse>.NotFoundResult($"menu with id: {idMenu} not found");
            }

            List<Domain.Entities.Order> orders = orderQuery.GetOrdersByMenu(idMenu);

            List<SummaryOrder> Orders = this.GetSummaryOrders(orders);

            var menuOrdersResponse = new MenuWithOrdersResponse
            {
                Data = this.GetMenuData(menu, orders),
                Orders = Orders
            };

            return Result<MenuWithOrdersResponse>.SuccessResult(menuOrdersResponse);
        }

        private MenuData GetMenuData(Domain.Entities.Menu Menu, List<Domain.Entities.Order> orders)
        {

            int totalOrders = orders.Count;
            int cancelledOrders = orders.Where(o => o.StateCode == (int)OrderState.Cancelled).Count();
            int inProgress = orders.Where(o => o.StateCode == (int)OrderState.InProgress).Count();
            int finishedOrdes = orders.Where(o => o.StateCode == (int)OrderState.Finished).Count();

            decimal totalRevenue = orders.Where(o => o.StateCode == (int)OrderState.Finished)
                                  .SelectMany(o => o.Items)
                                  .Sum(item => item.Quantity * item.MenuOption.Price);

            decimal totalAllOrders = orders
                                  .SelectMany(o => o.Items)
                                  .Sum(item => item.Quantity * item.MenuOption.Price);

            return new MenuData
            {
                Id = Menu.IdMenu,
                EatingDate = Menu.EatingDate,
                CloseDate = Menu.CloseDate,
                UploadDate = Menu.UploadDate,
                TotalOrders = totalOrders,
                InProgress = inProgress,
                CancelledOrders = cancelledOrders,
                FinishedOrders = finishedOrdes,
                TotalRevenue = totalRevenue,
                TotalAllOrders = totalAllOrders
            };
        }

        private List<SummaryOrderItemResponse> GetItemsResponse(Domain.Entities.Order order)
        {
            var itemsResponses = new List<SummaryOrderItemResponse>();

            foreach (var item in order.Items)
            {
                var itemResponse = new SummaryOrderItemResponse
                {
                    IdDish = item.IdDish,
                    Description = item.MenuOption.Dish.Description,
                    Quantity = item.Quantity
                };

                itemsResponses.Add(itemResponse);
            }

            return itemsResponses;
        }

        private List<SummaryOrder> GetSummaryOrders(List<Domain.Entities.Order> orders)
        {
            var Orders = new List<SummaryOrder>();

            foreach (var order in orders)
            {
                var summary = new SummaryOrder
                {
                    Id = order.IdOrder,
                    IdUser = order.IdUser,
                    Username = $"{order.User.Name} {order.User.LastName}",
                    State = order.State.Description,
                    Items = this.GetItemsResponse(order)
                };

                Orders.Add(summary);
            }

            return Orders;
        }
    }
}
