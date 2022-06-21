using System;

namespace YFTools.CommonComponents.Exceptions;

public class BusinessVerificationException : Exception
{
    public new string Message { get; set; }

    public BusinessVerificationException(string message)
    {
        Message = message;
    }
}