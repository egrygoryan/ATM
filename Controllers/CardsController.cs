using ATM.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ATM.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase 
{
    // Prefer to use interfaces instead concrete types
    private static readonly ICollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland","edyDfd5A", 800, CardBrands.Visa),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", 400, CardBrands.MasterCard)
    };

    [HttpGet("{cardNumber}/balance")]
    // Prefer to use IActionResult instead concrete type
    // Please use Http Attributes [FromRoute] etc. to show other your intention
    public IActionResult GetBalance([FromRoute] string cardNumber)
    {
        return Cards.FirstOrDefault(x => x.Nubmer == cardNumber)
            is { } card
            ? Ok(new {card.Nubmer, card.Balance})
            : BadRequest(new {Message = ""});
    }

    [HttpGet("{cardNumber}/init")]
    public IActionResult Init([FromRoute] string cardNumber)
    {
        return Accepted();
    }
    
    [HttpPost]
    public IActionResult Authorize([FromBody] object request)
        // Create CardAuthorizeRequest model with
        // CardNumber
        // CardPassword
    {
        // card.VerifyPassword(request.CardPassword);
        return Ok();
    }

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] object request) 
        // Create CardWithdrawRequest model with
        // CardNumber
        // Amount
    {
        // Prefer to use LINQ everywhere
        //
        // BadRequest("cannot withdraw money"); <- this is not valid JSON
        // You have to return at least -> new {} <- anonymous object

        // foreach (var card in _cards) {
        //     if (card.Id == cardNumber && card.Password == pass) {
        //         if (card.Balance < amount) {
        //             return BadRequest("cannot withdraw money");
        //         }
        //         card.Balance -= amount;
        //         return NoContent();
        //     }
        // }
        // return NotFound();
        return Ok();
    }
}
