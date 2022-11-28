namespace ATM.Models;

public sealed class Card 
{
    public string Number { get; }
    public string Holder { get; }
    private string Password { get; }
    public decimal Balance { get; private set; }
    public CardBrands Brand { get; }

    public Card(string number, string holder, string password, decimal balance, CardBrands brand)
    {
        Number = number;
        Holder = holder;
        Password = password;
        Balance = balance;
        Brand = brand;
    }

    public bool VerifyPassword(string password)
    {
        return Password == password;
    }

    public bool WithdrawFunds(decimal amount)
    {
        _ = amount <= 0
            ? throw new ArgumentOutOfRangeException(nameof(amount), "Amount can't be less or equal zero")
            : Balance < amount
                ? throw new ArgumentOutOfRangeException(nameof(Balance), "Balance can't be less than amount")
                : Balance -= amount;
        return true;
    }
}

