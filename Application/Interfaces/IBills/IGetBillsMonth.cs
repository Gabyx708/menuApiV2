using Application.UseCase.V2.Bills;

namespace Application.Interfaces.IBills
{
    public interface IGetBillsMonth
    {
        Result<BillsMonthResponse> GetBillsInMonth(int year, int month);
    }
}
