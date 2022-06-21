using System;

namespace YFTools.CommonComponents.Exceptions;

public class LoginVerificationException : Exception
{
    public new string Message { get; set; }

    public LoginVerificationException()
    {
        Message = string.Empty;
    }

    public LoginVerificationException(string message)
    {
        Message = message;
    }
}