using System;

namespace PostOffice.Application.Responses
{
    public class ErrorResponse
    {
        public ErrorMessage Error { get; set; }
        public ErrorResponse(int statusCode, string message):this(statusCode, message, String.Empty)
        {
            
        }
        public ErrorResponse(int statusCode, string message, string messageText)
        {
            Error = new ErrorMessage(){StatusCode = statusCode, Message = message, MessageText = messageText};
        }
        public  class ErrorMessage
        {
            public string Message { get; set; }
            public int StatusCode { get; set; }
            public string MessageText { get; set; }
        }
    }

}
