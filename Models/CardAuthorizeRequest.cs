namespace ATM.Models {
    public class CardAuthorizeRequest 
    {
        public string CardNumber { get; }
        public string CardPassword { get; }

        public CardAuthorizeRequest(string cardNumber, string cardPassword)
        {
            CardNumber = cardNumber;
            CardPassword = cardPassword;
        }
    }
}
