namespace CheckOut.Infrastructure.Executors.WebApi.Models
{
    public class WebApiCommandValidationError
    {
        public string Field { get; set; }

        public string Message { get; set; }

        public WebApiCommandValidationError(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}
