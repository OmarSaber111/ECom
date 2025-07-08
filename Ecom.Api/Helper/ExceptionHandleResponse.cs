namespace Ecom.Api.Helper
{
    public class ExceptionHandleResponse : ApiResponse
    {
        public ExceptionHandleResponse(int statusCode, string? statusMessage = null, string? details = null) : base(statusCode, statusMessage!)
        {
            Details = details;
        }
        public string? Details { get; set; }
    }
}
