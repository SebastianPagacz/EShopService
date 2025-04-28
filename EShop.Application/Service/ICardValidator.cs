using EShop.Domain.Enums;

namespace EShop.Application.Service
{
    public interface ICardValidator
    {
        CreditCardProvider GetCardType(string cardNumber);
        bool ValidateCard(string cardNumber);
    }
}