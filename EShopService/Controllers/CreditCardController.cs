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
    private readonly ICardValidator _cardValidator;

    public CreditCardController(ICardValidator cardValidator) 
    {
        _cardValidator = cardValidator;
    }
    [HttpGet]
    public IActionResult Get([FromQuery] string cardNumber)
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
