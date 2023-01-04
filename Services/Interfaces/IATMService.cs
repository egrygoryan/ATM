namespace ATM.Services.Interfaces;

public interface IATMService
{
    bool HasCard(string cardNumber);
    bool VerifyCard(string cardNumber, string password);
    (string CardNumber, decimal CardBalance) GetCardBalance(string cardNumber);
    void Withdraw(string cardNumber, decimal amount);
}