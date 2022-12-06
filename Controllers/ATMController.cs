using ATM.DTO;

namespace ATM.Controllers;

[ApiController]
[Route("api/cards")]
public class ATMController : ControllerBase 
{
    private readonly IATMService _cardService;
    public ATMController(IATMService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet("{cardNumber}/init")]
    public IActionResult Init([FromRoute] string cardNumber) =>
    _cardService.HasCard(cardNumber)
        ? Accepted()
        : NotFound();

    [HttpPost]
    public IActionResult Authorize([FromBody] CardAuthorizeRequest request) =>
        _cardService.VerifyCard(request)
            ? Ok()
            : Unauthorized(new { Message = "Card verification failed" });

    [HttpGet("{cardNumber}/balance")]
    public IActionResult GetBalance([FromRoute] string cardNumber)
    {
        if (_cardService.GetCardBalance(cardNumber) is not CardBalanceResponse response)
        {
            return BadRequest(new { Message = "Invalid card number" });
        }

        return Ok(response);
    }

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] CardWithdrawRequest request) =>
        _cardService.Withdraw(request)
            ? Ok(new { Message = "Operation completed successfully" })
            : BadRequest(new { Message = "Invalid card number" });
}
