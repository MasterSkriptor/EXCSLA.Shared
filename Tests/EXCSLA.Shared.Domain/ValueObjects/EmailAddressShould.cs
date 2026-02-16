using Xunit;
using EXCSLA.Shared.Domain.ValueObjects;
using EXCSLA.Shared.Domain.Exceptions;

namespace EXCSLA.Shared.Tests.Domain.UnitTests
{
    public class EmailAddressShould
    {
        [Fact]
        public void EmailAddressCreation()
        {
            var email = new Email("he@excsla.com");

            Assert.Equal("he", email.Address);
            Assert.Equal("excsla.com", email.Domain);
        }

        [Fact]
        public void EmailAddressOutOfBounds()
        {
            Assert.Throws<EmailAddressOutOfBoundsException>(() => new Email(""));
            Assert.Throws<EmailAddressOutOfBoundsException>(() => new Email("j.@server1.proseware.com"));
            Assert.Throws<EmailAddressOutOfBoundsException>(() => new Email("j..s@proseware.com"));
            Assert.Throws<EmailAddressOutOfBoundsException>(() => new Email("js*@proseware.com"));
            Assert.Throws<EmailAddressOutOfBoundsException>(() => new Email("js@proseware..com"));
            Assert.Throws<EmailAddressOutOfBoundsException>(() => new Email("lakjdhflajkdhfalkadfadfajdhfakjhdflakjdhflakdhflakjdhf@excsla.com"));
        }
    }
}