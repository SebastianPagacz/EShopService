using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EShop.Application;
using EShop.Domain.CreditCardExceptions;
using EShop.Domain.Enums;

namespace EShopService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditCardController : ControllerBase
{
    private readonly CardValidator _cardValidator;

    public CreditCardController(CardValidator cardValidator) 
    {
        _cardValidator = cardValidator;
    }
    internal IActionResult Get([FromBody] string cardNumber)
    {
        try
        {
            _cardValidator.ValidateCard(cardNumber);
            CreditCardProvider result = _cardValidator.GetCardType(cardNumber);
            return StatusCode(200, $"Card is valid and is of type {result}");
        }
        catch (CardNumberTooLongException)
        {
            return StatusCode(414);
        }
        catch (CardNumberTooShortException)
        {
            return StatusCode(400);
        }
        catch (CardNumberInvalidException)
        {
            return StatusCode(406);
        }
    }
}
