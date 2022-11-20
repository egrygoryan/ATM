namespace ATM.DTO {
    public class Card {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string  Password { get; set; }
        public decimal Balance { get; set; }
        public CardBrands CardBrand { get; set; }
    }
}
