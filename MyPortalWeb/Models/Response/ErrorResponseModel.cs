namespace MyPortalWeb.Models.Response
{
    public class ErrorResponseModel
    {
        public ErrorResponseModel(string error)
        {
            Error = error;
        }
        
        public string Error { get; set; }
    }
}