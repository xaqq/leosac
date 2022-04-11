namespace LeosacAPI;

public class LeosacException : ApplicationException
{
    
}

public class LeosacApiException : LeosacException
{
    public APIStatusCode StatusCode;
    public string Message;

    public LeosacApiException(APIStatusCode statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}

public class InvalidApiCredentialException : LeosacException
{
    
}