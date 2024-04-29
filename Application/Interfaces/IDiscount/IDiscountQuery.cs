using Domain.Entities;

namespace Application.Interfaces.IDiscount
{
    public interface IDiscountQuery
    {
        Discount GetLastestDiscount();
    }
}
