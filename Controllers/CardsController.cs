using ATM.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase 
{
    private static readonly ICollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", 800, CardBrands.Visa),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", 400, CardBrands.MasterCard)
    };

    [HttpGet("{cardNumber}/balance")]
    public IActionResult GetBalance([FromRoute] string cardNumber)
    {
        return Cards.FirstOrDefault(x => x.Number == cardNumber)
            is { } card
            ? Ok(new {card.Number, card.Balance})
            : BadRequest(new {Message = ""});
        //Even if card is initialized how can we guarantee,
        //that next uri request will contain valid, prev. initialized card?
        //e.g. I initialized card, authorized, then try to use getbalance
        // but misspelled one digit in a card route.
    }

    [HttpGet("{cardNumber}/init")]
    public IActionResult Init([FromRoute] string cardNumber)
    {
        return Cards.FirstOrDefault(x => x.Number == cardNumber)
            is { } card
            ? Accepted()
            : NotFound();
    }

    [HttpPost]
    public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
    {
        return Cards.FirstOrDefault(x => x.Number == request.CardNumber 
            && x.VerifyPassword(request.CardPassword))
                is { } card 
                ? Ok()
                : Unauthorized(new {Message = "Card verification failed" });
    }

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] CardWithdrawRequest request)
    {
        var card = Cards.FirstOrDefault(x => x.Number == request.CardNumber);

        if (card is null)
        {
            return BadRequest(new {Message = "Invalid card number"});
        }
        
        card.WithdrawFunds(request.Amount);

        return Ok(new {Message = "Operation completed successfully"});
    }
}
