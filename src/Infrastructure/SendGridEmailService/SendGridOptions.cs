namespace EXCSLA.Shared.Infrastructure
{
    public class SendGridOptions
    {
        private string _sendGridApiKey;
        private string _sendGridFromEmailAccount;
        private string _sendGridReplyToEmailAccount;

        public string Key { get { return _sendGridApiKey; } set { _sendGridApiKey = value; } }
        public string SendFromEmailAddress { get {return _sendGridFromEmailAccount; } set {_sendGridFromEmailAccount = value;}}
        public string ReplyToEmailAddress {get {return _sendGridReplyToEmailAccount;} set {_sendGridReplyToEmailAccount = value;}}

        public SendGridOptions() { }
    }
}
