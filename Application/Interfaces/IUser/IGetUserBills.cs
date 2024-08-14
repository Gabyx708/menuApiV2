using Application.UseCase.V2.User.GetBills;

namespace Application.Interfaces.IUser
{
    public interface IGetUserBills
    {
        Result<UserBillResponse> GetMonthBill(string idUser, int year, int month);
    }
}
