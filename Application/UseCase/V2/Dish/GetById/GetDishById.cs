using Application.Interfaces.IDish;
using Application.UseCase.V2.Menu.GetById;

namespace Application.UseCase.V2.Dish.GetById
{
    public class GetDishById : IGetDishByIdQuery
    {
        private readonly IDishQuery dishQuery;

        public GetDishById(IDishQuery dishQuery)
        {
            this.dishQuery = dishQuery;
        }

        Result<GetDishByIdResponse> IGetDishByIdQuery.GetDishById(string id)
        {
            var validator = new GetDishByIdValidation();
            var validationResult = validator.Validate(id);


            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result<GetDishByIdResponse>.ValidationResult(errorMessages);
            }


            try
            {
                int idDish = int.Parse(id);
                var dishFound = dishQuery.GetDishById(idDish);

                var dishResponse = new GetDishByIdResponse
                {
                    Id = dishFound.IdDish,
                    Description = dishFound.Description,
                    Price = dishFound.Price
                };

                return Result<GetDishByIdResponse>.SuccessResult(dishResponse);

            }catch(NullReferenceException)
            {
                return Result<GetDishByIdResponse>
                      .NotFoundResult($"the dish with id {id} was not found");
            }
            catch (Exception)
            {
                return Result<GetDishByIdResponse>
                              .FailureResult("An unexpected mistake occurred when consulting the dish");
            }

           
        }
    }
}
