using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.V2.Menu.Create
{
    internal class CreateMenuValidation : AbstractValidator<CreateMenuRequest>
    {
        public CreateMenuValidation()
        {
            RuleFor(cr => cr.CloseDate)
                    .GreaterThan(cr => cr.EatingDate)
                    .WithMessage("Close date must be after the eating date.");

            RuleFor(cr => cr.CloseDate)
                    .NotEqual(cr => cr.EatingDate)
                    .WithMessage("The two dates cannot be the same");
        }
    }
}
