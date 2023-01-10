using ATM.DTO;

namespace ATM.Controllers;

[ApiController]
[Route("api/cards")]
public class ATMController : ControllerBase
{
    private readonly IATMEventService _cardService;
    public ATMController(IATMEventService cardService) => _cardService = cardService;

    [HttpGet("{cardNumber}/init")]
    public IActionResult Init([FromRoute] string cardNumber) =>
    _cardService.HasCard(cardNumber)
        ? Accepted()
        : NotFound();

    [HttpPost]
    public IActionResult Authorize([FromBody] CardAuthorizeRequest request) =>
        _cardService.VerifyCard(request.CardNumber, request.CardPassword)
            ? Ok()
            : Unauthorized(new { Message = "Card verification failed" });

    [HttpGet("{cardNumber}/balance")]
    public IActionResult GetBalance([FromRoute] string cardNumber)
    {
        var balance = _cardService.GetCardBalance(cardNumber);
        return Ok(new {card = cardNumber, balance});
    }

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] CardWithdrawRequest request)
    {
        _cardService.Withdraw(request.CardNumber, request.Amount);
        return Ok(new { Message = "Operation completed succesfully" });
    }
}