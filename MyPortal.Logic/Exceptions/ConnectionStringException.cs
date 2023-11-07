namespace MyPortal.Logic.Exceptions
{
    public class ConnectionStringException : ConfigurationException
    {
        public ConnectionStringException(string message) : base(message)
        {
        }
    }
}