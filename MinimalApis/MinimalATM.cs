namespace ATM.MinimalApis
{
    public class MinimalATM
    {
        private readonly IATMService _cardService;
        public MinimalATM(IATMService cardService) => _cardService = cardService;
        public void Register(WebApplication app)
        {
            app.MapGet("minimalapi/cards/{cardNumber}/init", Init);
            app.MapPost("minimalapi/cards", Authorize);
            app.MapGet("minimalapi/cards/{cardNumber}/balance", GetBalance);
            app.MapPost("minimalapi/cards/withdraw", Withdraw);
        }

        private IResult Init([FromRoute] string cardNumber) =>
            _cardService.HasCard(cardNumber)
                ? Results.Accepted()
                : Results.NotFound();

        public IResult Authorize([FromBody] CardAuthorizeRequest request) =>
            _cardService.VerifyCard(request.CardNumber, request.CardPassword)
                ? Results.Ok()
                : Results.Unauthorized();

        public IResult GetBalance([FromRoute] string cardNumber)
        {
            var balance = _cardService.GetCardBalance(cardNumber);
            return Results.Ok(new
            {
                card = cardNumber,
                balance
            });
        }

        public IResult Withdraw([FromBody] CardWithdrawRequest request)
        {
            _cardService.Withdraw(request.CardNumber, request.Amount);
            return Results.Ok(new { Message = "Operation completed succesfully" });
        }
    }
}
