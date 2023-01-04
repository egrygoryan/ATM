namespace ATM.Services.Interfaces;

public interface IATMEventBroker
{
    void StartStream(string key, ATMEvent @event);
    void AppendEvent(string key, ATMEvent @event);
    ATMEvent? FindEvent<T>(string key) where T : ATMEvent;
    ATMEvent GetLastEvent(string key);
}
