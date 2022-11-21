namespace ATM.Models;

public sealed class Card 
{
    public string Nubmer { get; }
    public string Holder { get; }
    private string Password { get; }
    public decimal Balance { get; private set; }
    public CardBrands Brand { get; }

    public Card(string nubmer, string holder, string password, decimal balance, CardBrands brand)
    {
        Nubmer = nubmer;
        Holder = holder;
        Password = password;
        Balance = balance;
        Brand = brand;
    }
}

