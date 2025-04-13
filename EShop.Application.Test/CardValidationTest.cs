using EShop.Domain.CreditCardExceptions;
using EShop.Domain.Enums;
using Newtonsoft.Json.Linq;

namespace EShop.Application.Test;

public class CardValidationTests
{
    [Theory]
    [InlineData("3497 7965 8312 797", true)]
    [InlineData("2221032022465829", true)]
    [InlineData("378523393817437", true)]
    [InlineData("4024 0071 6540 1778", true)]
    [InlineData("345 - 470 - 784 - 783 - 010", true)]
    public void ValidateCard_CheckLength_ReturnsCorrectValue(string cardNumber, bool expected)
    {
        // Arange 
        var cardValidator = new CardValidator();

        // Act
        var result = cardValidator.ValidateCard(cardNumber);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("3497 7965 8312 797", CreditCardProvider.AmericanExpress)]
    [InlineData("5221032022465829", CreditCardProvider.MasterCard)]
    [InlineData("378523393817437", CreditCardProvider.AmericanExpress)]
    [InlineData("4024 0071 6540 1778", CreditCardProvider.Visa)]
    [InlineData("111 111 111", CreditCardProvider.None)]
    [InlineData("5530016454538418", CreditCardProvider.MasterCard)]
    public void GetCardType_ChecksValusesType_ReturnsCorrectType(string cardNumber, CreditCardProvider expectedResult)
    {
        // Arange 
        var cardValidator = new CardValidator();

        // Act
        var result = cardValidator.GetCardType(cardNumber);

        // Assert
        Assert.Equal(expectedResult, result);
    }


    [Fact]
    public void CardValidator_TooShort_ThrowsTooShortException()
    {
        // Arange
        var cardValidator = new CardValidator();

        // Act & Assert
        Assert.Throws<CardNumberTooShortException>(() => cardValidator.ValidateCard("111 111 111"));
    }
    [Fact]
    public void CardValidator_TooLong_ThrowsTooLongException()
    {
        // Arange
        var cardValidator = new CardValidator();

        // Act & Assert
        Assert.Throws<CardNumberTooLongException>(() => cardValidator.ValidateCard("111 111 111 111 111 111 111"));
    }
}