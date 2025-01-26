namespace StormEkspress.Helper
{
    public class EmailSendException : Exception
    {
        public EmailSendException(string message) : base(message) { }
    }
}
