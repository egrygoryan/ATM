namespace ATM.Controllers {
    public class CardWithdrawRequest 
    {
        public string CardNumber { get; }
        public decimal Amount { get; }

        public CardWithdrawRequest(string cardNumber, decimal amount)
        {
            CardNumber = cardNumber;
            Amount = amount;
        }
    }
}
