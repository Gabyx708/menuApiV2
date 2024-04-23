using Application.Common.Models;
using Application.Interfaces.IDish;

namespace Application.UseCase.V2.Dish.UpdatePrices
{
    public class DishesUpdatePrice : IDishesUpdatePrice
    {
        private readonly IDishQuery dishQuery;
        private readonly IDishCommand dishCommand;

        public DishesUpdatePrice(IDishQuery dishQuery, IDishCommand dishCommand)
        {
            this.dishQuery = dishQuery;
            this.dishCommand = dishCommand;
        }

        public Result<SystemResponse> UpdateDishesPrices(decimal price)
        {
            if (price < 3)
            {
                return Result<SystemResponse>.ValidationResult("price must be greather than 3");
            }

            int index = 1;
            var dishesPage = dishQuery.GetAll(index, 100);

            try
            {
                if (dishesPage.TotalPages == 1)
                {
                    this.UpdateDishesPage(dishesPage.Items, price);
                }

                for (int i = index; i <= dishesPage.TotalPages; i++)
                {
                    var page = dishQuery.GetAll(i, 100);
                    this.UpdateDishesPage(page.Items, price);
                }
            }
            catch (Exception)
            {
                return Result<SystemResponse>.FailureResult("The operation could not be completed");
            }

            var response = new SystemResponse
            {
                StatusCode = 200,
                Message = $"Updated the price of all dishes to ${price}"
            };

            return Result<SystemResponse>.SuccessResult(response);
        }

        private void UpdateDishesPage(List<Domain.Entities.Dish> dishes, decimal price)
        {
            foreach (var dish in dishes)
            {
                dish.Price = price;
                dishCommand.UpdateDish(dish);
            }
        }
    }
}
