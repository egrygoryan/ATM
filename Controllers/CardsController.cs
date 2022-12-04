using ATM.DTO;
using ATM.Services.Interfaces;

namespace ATM.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase 
{
    private readonly ICardService _cardService;
    public CardsController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet("{cardNumber}/balance")]
    public IActionResult GetBalance([FromRoute] string cardNumber)
    {
        //Not exactly sure that checing on card logic should be in controller 
        if (_cardService.FindCardByNumber(cardNumber) is not Card card)
        {
            return BadRequest(new { Message = "Can't get balance of the card" });
            //or better to send "Invalid card number?"
            //because current message is more appropriate to server error"
        }

        return Ok(new {card.Number, card.Balance});
    }

    [HttpGet("{cardNumber}/init")]
    // is it ok to use => expression for multiple rows?
    // or preferable to use return statement

    public IActionResult Init([FromRoute] string cardNumber) => 
        _cardService.Initialize(cardNumber)
            ? Accepted()
            : NotFound();

    [HttpPost]
    public IActionResult Authorize([FromBody] CardAuthorizeRequest request) =>
        _cardService.Authorize(request)
            ? Ok()
            : Unauthorized(new { Message = "Card verification failed" });

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] CardWithdrawRequest request) =>
        _cardService.Withdraw(request)
            ? Ok(new { Message = "Operation completed successfully" })
            : BadRequest(new { Message = "Invalid card number" });
}
