namespace EXCSLA.Shared.Core.ValueObjects.Common
{
    public enum AlertType
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark
    }

    public class Alert : ValueObject
    {
        public string Message { get; private set; }
        public AlertType Type { get; private set; }

        public Alert() { }

        public Alert(string message, AlertType type = AlertType.Info)
        {
            SetMessage(message);
            SetType(type);
        }

        private void SetMessage(string message)
        {
            this.Message = message;
        }

        private void SetType(AlertType type)
        {
            this.Type = type;
        }
    }
}
