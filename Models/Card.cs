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

    public bool CardVerifyPassword(string password) //can we use method name withour prefix Card?
                                                    //we already use card functionality
    {
        return Password == password;
    }

    public bool CardWithdrawFunds(decimal amount)
    {
        switch (Balance >= amount && amount > 0)
        {
            case true:
                Balance -= amount;
                return true;
            default:
                return false;
        }
    }
}

