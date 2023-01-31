namespace ATM.Endpoints;

public class MinimalATM
{
    public void Register(WebApplication app)
    {
        app.MapGet("api/v2/cards/{cardNumber}/init", Init);
        app.MapPost("api/v2/cards/", Authorize);
        app.MapGet("api/v2/cards/{cardNumber}/balance", GetBalance);
        app.MapPost("api/v2/cards/withdraw", Withdraw);
    }


    private static IResult Init([FromRoute] string cardNumber, 
                                [FromServices] IATMService cardService) =>
        cardService.HasCard(cardNumber)
            ? Results.Accepted()
            : Results.NotFound();

    private static IResult Authorize([FromBody] CardAuthorizeRequest request, 
                                     [FromServices] IATMService cardService) =>
        cardService.VerifyCard(request.CardNumber, request.CardPassword)
            ? Results.Ok()
            : Results.Unauthorized();

    private static IResult GetBalance([FromRoute] string cardNumber,
                                      [FromServices] IATMService cardService)
    {
        var balance = cardService.GetCardBalance(cardNumber);
        return Results.Ok(new
        {
            card = cardNumber,
            balance
        });
    }

    private static IResult Withdraw([FromBody] CardWithdrawRequest request,
                                    [FromServices] IATMService cardService)
    {
        cardService.Withdraw(request.CardNumber, request.Amount);
        return Results.Ok(new { Message = "Operation completed succesfully" });
    }
}
