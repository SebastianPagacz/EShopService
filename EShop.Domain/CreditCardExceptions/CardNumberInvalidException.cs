﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.CreditCardExceptions;

public class CardNumberInvalidException : Exception
{
    public CardNumberInvalidException() : base() { }
    public CardNumberInvalidException(string message) : base(message) { }
    public CardNumberInvalidException(string message, Exception innerException) : base(message, innerException) { }
}
