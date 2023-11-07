namespace MyPortalWeb.Models.Response
{
    public class StringResponseModel
    {
        public StringResponseModel(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}