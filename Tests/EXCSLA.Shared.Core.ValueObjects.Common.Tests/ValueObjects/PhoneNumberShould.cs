using Xunit;
using EXCSLA.Shared.Core.ValueObjects;
using EXCSLA.Shared.Core.Exceptions;

namespace EXCSLA.Shared.Tests.Core.UnitTests
{
    public class PhoneNumberShould
    {
        [Fact]
        public void CreatePhoneNumber()
        {
            var phone1 = new PhoneNumber(PhoneType.Mobile, "985", "445", "3111");
            var phone2 = new PhoneNumber(PhoneType.Mobile, "(985) 445-3111");
            var phone3 = new PhoneNumber(PhoneType.Mobile, "9854453111");
            var phone4 = new PhoneNumber(PhoneType.Mobile, "9854453111    ");
            var phone5 = new PhoneNumber(PhoneType.Mobile, "985-445-3111");

            Assert.Equal(phone1, phone2);
            Assert.Equal(phone1, phone3);
            Assert.Equal(phone1, phone4);
            Assert.Equal(phone1, phone5);

        }

        [Fact]
        public void InvalidEntries()
        {
            Assert.Throws<PhoneNumberOutOfBoundsException>(() => new PhoneNumber(PhoneType.Mobile, "985", "445", "31111"));
            Assert.Throws<PhoneNumberOutOfBoundsException>(() => new PhoneNumber(PhoneType.Mobile, "9851", "445", "3111"));
            Assert.Throws<PhoneNumberOutOfBoundsException>(() => new PhoneNumber(PhoneType.Mobile, "985", "4451", "3111"));
            Assert.Throws<PhoneNumberOutOfBoundsException>(() => new PhoneNumber(PhoneType.Mobile, "9854453111s"));
            Assert.Throws<PhoneNumberOutOfBoundsException>(() => new PhoneNumber(PhoneType.Mobile, "985445SSSS"));
        }

        [Fact]
        public void PhoneNumberToString()
        {
            var phone1 = new PhoneNumber(PhoneType.Mobile, "985", "445", "3111");

            Assert.Equal("(985) 445-3111", phone1.ToString());
        }
    }
}