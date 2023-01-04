namespace ATM.Services;

public sealed class ATMEventBroker : IATMEventBroker
{
    private static readonly Dictionary<string, List<ATMEvent>> _events = new ();
    private static KeyNotFoundException StreamNotFound => new ("Stream not found");
    
    public void StartStream(string key, ATMEvent @event)
    {
        if (_events.ContainsKey(key))
        {
            _events.Remove(key);
        }

        _events[key] = new () {@event};
    }

    public void AppendEvent(string key, ATMEvent @event)
    {
        if (!_events.TryGetValue(key, out var events))
        {
            throw StreamNotFound;
        }
        
        events.Add(@event);
    }
    
    public ATMEvent? FindEvent<T>(string key) where T : ATMEvent
    {
        if (_events.TryGetValue(key, out var events))
        {
            return events.FirstOrDefault(x => x is T);
        }

        throw StreamNotFound;
    }

    public ATMEvent GetLastEvent(string key)
    {
        if (_events.TryGetValue(key, out var events))
        {
            return events.Last();
        }
        
        throw StreamNotFound;
    }
    
    //public void RemoveStream(string key) => _cardEvents.Remove(key);

    //what for is this method?
    //public ATMEvent? FindEvent<T>(string key) where T : ATMEvent
    //    => _cardEvents.ContainsKey(key)
    //        ? _cardEvents[key].FirstOrDefault(x => x is T)
    //        : throw new KeyNotFoundException("Unexpected key value");
}