using EXCSLA.Shared;
using EXCSLA.Shared.Core.Exceptions;
using System.Linq;

namespace EXCSLA.Shared.Core.ValueObjects.Common
{
    public enum PhoneType
    {
        Home = 0,
        Mobile = 1,
        Work = 2
    }

    public class PhoneNumber : ValueObject
    {
        public PhoneType Type {get; private set;}
        public string AreaCode {get; private set;}
        public string Prefix {get; private set;}
        public string Suffix {get; private set;}

        public PhoneNumber(PhoneType type, string areaCode, string prefix, string suffix)
        {
            this.Type = type;
            SetAreaCode(areaCode);
            SetPrefix(prefix);
            SetSuffix(suffix);
        }

        public PhoneNumber(PhoneType type, string phoneNumber)
        {
            // Trim any spaces from phone number.
            phoneNumber = phoneNumber.Replace(" ", "");
            phoneNumber = phoneNumber.Replace("(", "");
            phoneNumber = phoneNumber.Replace(")", "");
            phoneNumber = phoneNumber.Replace("-", "");
            
            if(phoneNumber.Length != 10) throw new PhoneNumberOutOfBoundsException("A phone number must contain ten (10) numbers valued 0 through 9.");
            var chars = phoneNumber.ToCharArray();
            for(int i = 0; i < chars.Length; i++)
            {
                if (!char.IsDigit(chars[i])) throw new PhoneNumberOutOfBoundsException("A phone number must contain ten(10) numbers valued 0 through 9.");
            }

            var areaCode = phoneNumber.Substring(0, 3);
            var prefix = phoneNumber.Substring(3, 3);
            var suffix = phoneNumber.Substring(6, 4);

            this.Type = type;
            SetAreaCode(areaCode);
            SetPrefix(prefix);
            SetSuffix(suffix);
        }

        private void SetPrefix(string prefix)
        {
            if(prefix.Length != 3)
                throw new PhoneNumberOutOfBoundsException("The Prefix must have 3 numeric digits.");
            
            Prefix = prefix;
        }

        private void SetAreaCode(string areaCode)
        {
            if(areaCode.Length != 3)
                throw new PhoneNumberOutOfBoundsException("The Area Code must have 3 numeric digits.");
            
            AreaCode = areaCode;
        }

        private void SetSuffix(string suffix)
        {
            if(suffix.Length != 4)
                throw new PhoneNumberOutOfBoundsException("The suffix must have 4 numeric digits.");
                Suffix = suffix;
        }

        public override string ToString()
        {
            return "(" + this.AreaCode + ") " + this.Prefix + "-" + this.Suffix;
        }

    }
}