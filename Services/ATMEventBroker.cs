namespace ATM.Services;

public class ATMEventBroker : IATMEventBroker
{
    private readonly static IDictionary<string, ICollection<ATMEvent>> _cardEvents
        = new Dictionary<string, ICollection<ATMEvent>>();

    public void StartStream(string key, ATMEvent @event)
    {
        if (_cardEvents.ContainsKey(key))
        {
            RemoveStream(key);
        }
        _cardEvents.Add(key, new List<ATMEvent>() { @event });
    }

    public void AppendEvent(string key, ATMEvent @event) => _cardEvents[key].Add(@event);


    public ATMEvent? GetLastEvent(string key)
        => _cardEvents.ContainsKey(key)
            ? _cardEvents[key].Last()
            : null;
    public void RemoveStream(string key) => _cardEvents.Remove(key);

    //what for is this method?
    //public ATMEvent? FindEvent<T>(string key) where T : ATMEvent
    //    => _cardEvents.ContainsKey(key)
    //        ? _cardEvents[key].FirstOrDefault(x => x is T)
    //        : throw new KeyNotFoundException("Unexpected key value");
}