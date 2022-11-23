using ATM.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
            : BadRequest(new {Message = ""}); //why don't we use NotFound, BadRequest means
                                              //something wrong in syntax of our request
    }

    [HttpGet("{cardNumber}/init")]
    public IActionResult Init([FromRoute] string cardNumber)
    {
        return Accepted();
    }

    [HttpPost]
    public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
    {
        return Cards.FirstOrDefault(x => x.Number == request.CardNumber)
            is { } card
            ? card.CardVerifyPassword(request.CardPassword)
                ? Ok()
                : BadRequest(new { Message = "Invalid password" })
            : NotFound(new { Message = "Invalid card number" });
    }

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] CardWithdrawRequest request) 
    {
        return Cards.FirstOrDefault(x => x.Number == request.CardNumber)
            is { } card
            ? card.CardWithdrawFunds(request.Amount)
                // should we return here smth like CreateAtRoute/Action() instead of OK()?
                // in future iterations for transactions e.g.
                ? Ok() 
                : BadRequest(new { Message = "Operation can't be performed" })
            : NotFound(new { Message = "Invalid card number" });
    }
}
