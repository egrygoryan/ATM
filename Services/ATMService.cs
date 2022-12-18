namespace ATM.Services;

public class ATMService : IATMService
{
    private  static readonly ICollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", 800, CardBrands.Visa),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", 4000, CardBrands.MasterCard)
    };

    private static readonly ICollection<WithdrawLimits> Limits = new List<WithdrawLimits>
    {
        new (CardBrands.Visa, 200),
        new (CardBrands.MasterCard, 300)
    };

    private decimal TotalAmount { get; set; } = 10_000;

    public bool HasCard(string cardNumber) => Cards.Any(c => c.VerifyNumber(cardNumber));

    public bool VerifyCard(string cardNumber, string password) =>
        Cards.Any(c => c.VerifyNumber(cardNumber) 
            && c.VerifyPassword(password));

    public void Withdraw(string cardNumber, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("You could not withdraw less or equal to zero");
        }

        if (TotalAmount <= 0)
        {
            throw new InvalidOperationException("ATM balance is less or equals zero");
        }

        if (TotalAmount < amount)
        {
            throw new InvalidOperationException("Insufficient funds in ATM");
        }

        var card = Cards.First(c => c.VerifyNumber(cardNumber));
        var limit = Limits.First(l => l.CardBrand == card.Brand).Limit;

        if (amount > limit)
        {
            throw new InvalidOperationException("You could not withdraw more than limit");
        }

        TotalAmount -= amount;
        card.WithdrawFunds(amount);
    }
    public (string, decimal) GetCardBalance(string cardNumber)
    {
        var card = Cards.First(c => c.VerifyNumber(cardNumber));
        return new (cardNumber, card.GetBalance());
    }
}