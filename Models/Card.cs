namespace ATM.Models;

public sealed class Card 
{
    public string Number { get; }
    public string Holder { get; }
    private string Password { get; }
    private decimal Balance { get; set; }
    public CardBrands Brand { get; }

    public Card(string number, string holder, string password, decimal balance, CardBrands brand)
    {
        Number = number;
        Holder = holder;
        Password = password;
        Balance = balance;
        Brand = brand;
    }

    public bool VerifyPassword(string password) => Password == password;

    public bool VerifyNumber(string number) => Number == number;

    public void WithdrawFunds(decimal amount)
    {
        if (Balance <= 0)
        {
            throw new InvalidOperationException("Your balance is less or equals zero");
        }

        if (Balance < amount)
        {
            throw new InvalidOperationException($"You don't have enough cash to withdraw {amount}");
        }

        Balance -= amount;
    }

    public decimal GetBalance() => Balance;
    public override bool Equals(object? obj) => Equals(obj as Card);
    private bool Equals(Card? card) => card is not null && card.Number == Number;
    public override int GetHashCode() => string.GetHashCode(Number);
}