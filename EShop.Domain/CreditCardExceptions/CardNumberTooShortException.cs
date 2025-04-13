using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.CreditCardExceptions;

public class CardNumberTooShortException : Exception
{
    public CardNumberTooShortException() : base() { }
    public CardNumberTooShortException(string message) : base(message) { }
    public CardNumberTooShortException(string message, Exception innerException) : base(message, innerException) { }
}
