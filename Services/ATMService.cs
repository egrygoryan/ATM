using ATM.DTO;

namespace ATM.Services;

public class ATMService : IATMService
{
    private static readonly ICollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", 800, CardBrands.Visa),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", 4000, CardBrands.MasterCard)
    };
    private decimal TotalAmount { get; set; } = 10_000;

    public bool HasCard(string cardNumber) => Cards.Any(c => c.VerifyNumber(cardNumber));

    public bool VerifyCard(CardAuthorizeRequest request) =>
        Cards.Any(c => c.VerifyNumber(request.CardNumber) 
            && c.VerifyPassword(request.CardPassword));

    public bool Withdraw(CardWithdrawRequest request)
    {
        var card = Cards.FirstOrDefault(c => c.Number == request.CardNumber);
        if (card is null)
        {
            return false;
        }

        card.WithdrawFunds(request.Amount);
        ATMWithdraw(request.Amount);

        return true;
    }

    public CardBalanceResponse? GetCardBalance(string cardNumber) 
    {
        var card = Cards.FirstOrDefault(c => c.VerifyNumber(cardNumber));

        return card switch
        {
            Card => new CardBalanceResponse(cardNumber, card.GetBalance()),
            _ => null 
        };
    }

    //Prob. should be placed into ATM class. (next iterations) 
    private void ATMWithdraw(decimal amount)
    {
        if (TotalAmount <= 0)
        {
            throw new InvalidOperationException("ATM balance is less or equals zero");
        }

        if (TotalAmount < amount)
        {
            throw new InvalidOperationException($"Insufficient funds in ATM");
        }
        TotalAmount -= amount;
    }
}