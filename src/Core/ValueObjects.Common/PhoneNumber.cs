using EXCSLA.Shared;
using EXCSLA.Shared.Core.Exceptions;
using System.Linq;

namespace EXCSLA.Shared.Core.ValueObjects.Common
{
    /// <summary>
    /// Phone Type is an enum used to describe the type of phone number in use. Home, Mobile, Work, Fax, etc.
    /// </summary>
    public enum PhoneType
    {
        Home = 0,
        Mobile = 1,
        Work = 2,
        Fax = 3
    }

    /// <summary>
    /// A standard US based phone number. Because this is a 
    /// value object, any changes to this object should result in a new object creation. Thus there is no
    /// public setting of properties.
    /// </summary>
    public class PhoneNumber : ValueObject
    {
        public PhoneType Type {get; private set;}
        public string AreaCode {get; private set;}
        public string Prefix {get; private set;}
        public string Suffix {get; private set;}

        /// <summary>
        /// This is an entity framework required constructor, and should not be used by the programmer. Because
        /// this is a value object there is not way to set the values of its properties, making this constructor 
        /// usesless to anyone other than ORM's.
        /// </summary>
        public PhoneNumber() { } // Required by EF

        /// <summary>
        /// Creates a standard US based phone number value object
        /// </summary>
        /// <param name="type">The type of phone number. Mobile, Home, Work, Fax, ect.</param>
        /// <param name="areaCode">The area code portion of a phone number.</param>
        /// <param name="prefix">The prefix portion of a phone number.</param>
        /// <param name="suffix">The suffix portion of a phone number.</param>
        public PhoneNumber(PhoneType type, string areaCode, string prefix, string suffix)
        {
            this.Type = type;
            SetAreaCode(areaCode);
            SetPrefix(prefix);
            SetSuffix(suffix);
        }

        /// <summary>
        /// Creates a stanard US based phone number value object
        /// </summary>
        /// <param name="type">The type of phone number. Mobile, Home, Work, Fax, ect.</param>
        /// <param name="phoneNumber">A string containing the entire phone number. Areacode, prefix, suffix. The format for
        /// this can be the entire number, 1234567890, or formatted (123)456-7890.
        /// </param>
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

        /// <summary>
        /// Returns a string containing the formatted phone number: eg. (123)456-7890
        /// </summary>
        /// <returns>example: (123)456-7890</returns>
        public override string ToString()
        {
            return "(" + this.AreaCode + ") " + this.Prefix + "-" + this.Suffix;
        }

    }
}