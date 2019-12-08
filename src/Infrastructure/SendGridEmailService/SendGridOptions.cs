namespace EXCSLA.Shared.Infrastructure
{
    public class SendGridOptions
    {
        private string _sendGridApiKey;

        public string Key { get { return _sendGridApiKey; } set { _sendGridApiKey = value; } }

        public SendGridOptions() { }
    }
}
