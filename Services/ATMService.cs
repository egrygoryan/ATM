namespace ATM.Services;

public class ATMService : IATMService
{
    private static readonly ICollection<Card> Cards = new List<Card>
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

    private readonly IATMEventBroker _eventBroker;

    public ATMService(IATMEventBroker eventBroker) => _eventBroker = eventBroker;

    public bool HasCard(string cardNumber)
    {
        var hasCard = Cards.Any(c => c.VerifyNumber(cardNumber));
        if (hasCard)
        {
            _eventBroker.StartStream(cardNumber, new InitEvent());
        }

        return hasCard;
    }

    public bool VerifyCard(string cardNumber, string password)
    {
        var @event = _eventBroker.FindEvent<InitEvent>(cardNumber);

        if (@event is null)
        {
            throw new InvalidOperationException("Initialize card to perform authorization");
        }
        
        var isVerifiedCard = Cards.Any(c => c.VerifyNumber(cardNumber)
            && c.VerifyPassword(password));

        if (isVerifiedCard)
        {
            _eventBroker.AppendEvent(cardNumber, new AuthorizeEvent());
        }

        return isVerifiedCard;
    }

    public void Withdraw(string cardNumber, decimal amount)
    {
        if (_eventBroker.GetLastEvent(cardNumber) is not AuthorizeEvent)
        {
            throw new InvalidOperationException("Could not perform unauthorized operation");
        }

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
        
        _eventBroker.AppendEvent(cardNumber, new WithdrawEvent());
    }
    public (string CardNumber, decimal CardBalance) GetCardBalance(string cardNumber)
    {
        if (_eventBroker.GetLastEvent(cardNumber) is not AuthorizeEvent)
        {
            throw new InvalidOperationException("Could not perform unauthorized operation");
        }
        
        var card = Cards.First(c => c.VerifyNumber(cardNumber));
        
        _eventBroker.AppendEvent(cardNumber, new GetBalanceEvent());

        return (CardNumber: cardNumber, CardBalance: card.GetBalance());
    }
}