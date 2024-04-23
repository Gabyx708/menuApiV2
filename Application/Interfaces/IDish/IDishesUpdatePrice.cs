using Application.Common.Models;

namespace Application.Interfaces.IDish
{
    public interface IDishesUpdatePrice
    {
        Result<SystemResponse> UpdateDishesPrices(decimal price);

    }
}
