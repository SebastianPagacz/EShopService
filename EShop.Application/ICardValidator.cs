using EShop.Domain.Enums;

namespace EShop.Application
{
    public interface ICardValidator
    {
        CreditCardProvider GetCardType(string cardNumber);
        bool ValidateCard(string cardNumber);
    }
}