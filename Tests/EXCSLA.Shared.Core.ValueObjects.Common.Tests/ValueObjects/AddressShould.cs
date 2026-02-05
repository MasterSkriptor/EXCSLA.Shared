using Xunit;
using System;
using EXCSLA.Shared.Core.Exceptions;
using EXCSLA.Shared.Core.ValueObjects;

namespace EXCSLA.Shared.Tests.Core.UnitTests
{
    public class AddressShould
    {
        [Fact]
        public void AddressCreation()
        {
            var address = new Address("12345 John Smith Rd", "", "Mandeville", "LA", "70471");

            Assert.Equal("12345 John Smith Rd", address.Address1);
            Assert.Equal("", address.Address2);
            Assert.Equal("Mandeville", address.City);
            Assert.Equal("LA", address.State);
            Assert.Equal("70471", address.Zip);
        }

        [Fact]
        public void AddressOutOfBounds()
        {
            Assert.Throws<ArgumentException>(() => new Address("", "", "Mandeville", "LA", "70471"));
            Assert.Throws<ArgumentException>(() => new Address("12345 John Smith Rd", "", "", "LA", "70471"));
            Assert.Throws<ArgumentException>(() => new Address("12345 John Smith Rd", "", "Mandeville", "", "70471"));
            Assert.Throws<MinimumLengthExceededException>(() => new Address("12345 John Smith Rd", "", "Mandeville", "L", "70471"));
            Assert.Throws<MaximumLengthExceededException>(() => new Address("12345 John Smith Rd", "", "Mandeville", "LAl", "70471"));
            Assert.Throws<ArgumentException>(() => new Address("12345 John Smith Rd", "", "Mandeville", "LA", ""));

        }
    }
}