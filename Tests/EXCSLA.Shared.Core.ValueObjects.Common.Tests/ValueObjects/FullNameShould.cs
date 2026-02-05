using Xunit;
using EXCSLA.Shared.Core.ValueObjects;

namespace EXCSLA.Shared.Tests.Core.UnitTests
{
    public class FullNameShould
    {
        [Fact]
        public void FullNameCreation()
        {
            var name = new FullName("John", "Doe");

            Assert.Equal("John Doe", name.ToString());
            Assert.Equal("John Doe", name.AsFormatedName());
            Assert.Equal("Doe, John", name.AsFormatedName(true));
        }

        [Fact]
        public void FullNameWithMiddleCreation()
        {
            var name = new FullName("John", "Adam", "Doe");

            Assert.Equal("John Doe", name.ToString());
            Assert.Equal("John Doe", name.AsFormatedName());
            Assert.Equal("Doe, John", name.AsFormatedName(true));
            Assert.Equal("John Adam Doe", name.AsFormatedName(false, true, false));
            Assert.Equal("John A. Doe", name.AsFormatedName(false, false, true));
        }
    }
}