namespace ATM.Services;

public sealed class ATMEventService : IATMEventService
{
    private readonly IATMService _atmService;
    private readonly IATMEventBroker _eventBroker;

    public ATMEventService(IATMService atmService, IATMEventBroker eventBroker) =>
        (_atmService, _eventBroker) = (atmService, eventBroker);

    public bool HasCard(string cardNumber)
    {
        var hasCard = _atmService.HasCard(cardNumber);
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

        var isVerifiedCard = _atmService.VerifyCard(cardNumber, password);
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

        _atmService.Withdraw(cardNumber, amount);

        _eventBroker.AppendEvent(cardNumber, new WithdrawEvent());
    }
    public decimal GetCardBalance(string cardNumber)
    {
        if (_eventBroker.GetLastEvent(cardNumber) is not AuthorizeEvent)
        {
            throw new InvalidOperationException("Could not perform unauthorized operation");
        }
        var cardBalance = _atmService.GetCardBalance(cardNumber);

        _eventBroker.AppendEvent(cardNumber, new GetBalanceEvent());
        return cardBalance;
    }
}
