using EShop.Domain.CreditCardExceptions;
using EShop.Domain.Enums;
using System.Text.RegularExpressions;

namespace EShop.Application;

public class CardValidator : ICardValidator
{
    // Checks if provided card number is valid with Luhna algorythm
    public bool ValidateCard(string cardNumber)
    {
        cardNumber = cardNumber.Replace(" ", "").Replace("-", "");
        if (!cardNumber.All(char.IsDigit))
            throw new CardNumberInvalidException();

        if (cardNumber.Length <= 13)
            throw new CardNumberTooShortException();

        if (cardNumber.Length >= 19)
            throw new CardNumberTooLongException();

        int sum = 0;
        bool alternate = false;

        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int digit = cardNumber[i] - '0';

            if (alternate)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            alternate = !alternate;
        }

        return (sum % 10 == 0);
    }

    public CreditCardProvider GetCardType(string cardNumber)
    {
        cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

        if (Regex.IsMatch(cardNumber, @"^4(\d{12}|\d{15}|\d{18})$"))
            return CreditCardProvider.Visa;
        else if (Regex.IsMatch(cardNumber, @"^(5[1-5]\d{14}|2(2[2-9][1-9]|2[3-9]\d{2}|[3-6]\d{3}|7([01]\d{2}|20\d))\d{10})$"))
            return CreditCardProvider.MasterCard;

        if (Regex.IsMatch(cardNumber, @"^3[47]\d{13}$"))
            return CreditCardProvider.AmericanExpress;

        return CreditCardProvider.None;
    }
}
