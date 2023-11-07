namespace MyPortal.Logic.Models.Web
{
    public class WebAction
    {
        public WebAction(string text, string url)
        {
            Text = text;
            Url = url;
        }

        public string Text { get; set; }
        public string Url { get; set; }
    }
}