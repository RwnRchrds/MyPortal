using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Misc
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