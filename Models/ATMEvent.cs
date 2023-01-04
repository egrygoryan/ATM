namespace ATM.Models;

public abstract record ATMEvent;
public record InitEvent : ATMEvent;
public record AuthorizeEvent : ATMEvent;
public record WithdrawEvent : ATMEvent;
public record GetBalanceEvent : ATMEvent;