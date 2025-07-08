namespace Ecom.Api.Helper
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string statusMessage=null)
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage ?? GetMessagefromStatusCode(statusCode);
        }
        private string GetMessagefromStatusCode(int statuscode)
        {
            switch (statuscode)
            {
                case 200:
                    return "Done";
                case 400:
                    return "Bad Request";
                case 401:
                    return "Un Authorized";
                case 404:
                    return "resources not found";
                case 500:
                    return "Server Error";
                default:
                    return null;
            }
        }

        public int StatusCode {  get; set; }
        public string? StatusMessage { get; set; }
    }
}
