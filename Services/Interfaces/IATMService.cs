using ATM.DTO;

namespace ATM.Services.Interfaces;

public interface IATMService
{
    bool HasCard(string cardNumber);
    bool VerifyCard(CardAuthorizeRequest request);
    CardBalanceResponse? GetCardBalance(string cardNumber);
    bool Withdraw(CardWithdrawRequest request);
}