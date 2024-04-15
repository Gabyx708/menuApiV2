using Application.Response.GenericResponses;

namespace Application.Exceptions
{
    public class SystemExceptionApp : Exception
    {
        public string _message;
        public SystemResponse _response;
        public SystemExceptionApp(string message, int code)
        {
            _message = message;
            _response = new SystemResponse
            {
                StatusCode = code,
                Message = message
            };
        }

    }
}
