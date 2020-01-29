namespace MyPortal.Logic.Models.Business
{
    public enum ResponseType
    {
        Ok,
        NotFound,
        BadRequest
    }
    public class ProcessResponse<T>
    {
        public ResponseType ResponseType { get; set; }
        public string ResponseMessage { get; set; }
        public T ResponseObject { get; set; }

        public ProcessResponse(ResponseType responseType, string responseMessage, T responseObject)
        {
            ResponseType = responseType;
            ResponseMessage = responseMessage;
            ResponseObject = responseObject;
        }
    }
}