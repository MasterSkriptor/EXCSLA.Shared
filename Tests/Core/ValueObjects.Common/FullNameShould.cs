using Xunit;
using EXCSLA.Shared.Core.ValueObjects;
using EXCSLA.Shared.Core.ValueObjects.Common;

namespace EXCSLA.Shared.Tests.Core.UnitTests
{
    public class FullNameShould
    {
        public void FullNameCreation()
        {
            var name = new FullName("John", "Yrle");

            Assert.Equal("John Yrle", name.ToString());
            Assert.Equal("John Yrle", name.AsFormatedName());
            Assert.Equal("Yrle, John", name.AsFormatedName(true));
        }
    }
}