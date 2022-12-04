using ATM.DTO;
using ATM.Services.Interfaces;

namespace ATM.Services;

public class CardService : ICardService
{
    //Should be moved to Repo
    private static readonly ICollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", 800, CardBrands.Visa),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", 400, CardBrands.MasterCard)
    };

    public bool Initialize(string cardNumber) =>
        FindCardByNumber(cardNumber) is not null;

    //This method should be placed in repository class
    public Card? FindCardByNumber(string cardNumber) =>
        Cards.FirstOrDefault(x => x.Number == cardNumber);

    public bool Authorize(CardAuthorizeRequest request)
    {
        return Cards.FirstOrDefault(x => x.Number == request.CardNumber
            && x.VerifyPassword(request.CardPassword)) is not null;
    }

    public bool Withdraw(CardWithdrawRequest request)
    {
        var card = FindCardByNumber(request.CardNumber);
     
        if(card is null)
        {
            return false;
        }

        card.WithdrawFunds(request.Amount);
        return true;
    }
}