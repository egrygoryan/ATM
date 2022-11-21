using ATM.DTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ATM.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase {
        private static readonly List<Card> _cards = new List<Card>
        {
            new Card{
                Id = "4444333322221111",
                FullName = "Troy Mcfarland",
                Password = "edyDfd5A",
                CardBrand = CardBrands.Visa,
                Balance = 800
            },

            new Card{
                Id = "5200000000001005",
                FullName = "Levi Downs",
                Password = "teEAxnqg",
                CardBrand = CardBrands.MasterCard,
                Balance = 400
            }
        };

        [HttpGet("{cardNumber}")]
        public ActionResult GetBalance(string cardNumber, [Required] string pass) {
            foreach (var card in _cards) {
                if (card.Id == cardNumber && card.Password == pass) {
                    return Ok(card.Balance);
                }
            }
            return NotFound();
        }

        [HttpPut("{cardNumber}")]
        public ActionResult PutBalance(string cardNumber, [Required] string pass, [Required] decimal amount) {
            foreach (var card in _cards) {
                if (card.Id == cardNumber && card.Password == pass) {
                    if (card.Balance < amount) {
                        return BadRequest("cannot withdraw money");
                    }
                    card.Balance -= amount;
                    return NoContent();
                }
            }
            return NotFound();
        }
    }
}
