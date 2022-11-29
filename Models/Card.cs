﻿namespace ATM.Models;

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

    public void WithdrawFunds(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("You could not withdraw less or equal to zero");
        }

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
}

