using MyPortal.Logic.Models.Exceptions;

namespace MyPortal.Logic.Models.Business
{
    public class ValidationResponse
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }

        public ValidationResponse(bool valid, string errorMessage = null)
        {
            Valid = valid;
            ErrorMessage = errorMessage;
        }

        public void Handle()
        {
            if (!Valid)
            {
                throw new ServiceException(ExceptionType.BadRequest, ErrorMessage);
            }
        }
    }
}