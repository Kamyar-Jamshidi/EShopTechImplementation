namespace EShopTI.Product.Command.Api.Models.ViewModels
{
    public class ApiResponse
    {
        public ResponseStatus Status { get; }
        public object Data { get; }
        public IEnumerable<string> Errors { get; }

        public ApiResponse(ResponseStatus status, object data = null, params string[] errors)
        {
            Status = status;
            Data = data;
            Errors = errors;
        }
    }

    public enum ResponseStatus
    {
        Ok = 200,
        AccessDenied = 403,
        NotFound = 404,
        InternalServerError = 500,
        BadRequest = 400
    }
}
