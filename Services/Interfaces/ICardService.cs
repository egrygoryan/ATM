using ATM.DTO;

namespace ATM.Services.Interfaces;

public interface ICardService
{
    bool Initialize(string cardNumber);

    // This method should be declared In IRepository interface
    Card? FindCardByNumber(string cardNumber);
    bool Withdraw(CardWithdrawRequest request);
    bool Authorize(CardAuthorizeRequest request);
}