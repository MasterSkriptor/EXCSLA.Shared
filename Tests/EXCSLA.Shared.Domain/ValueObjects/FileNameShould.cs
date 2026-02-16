using EXCSLA.Shared.Domain.ValueObjects;
using EXCSLA.Shared.Tests.Domain.Factories;
using Xunit;

namespace EXCSLA.Shared.Tests.Domain.UnitTests
{
    public class FileNameShould
    {
        [Fact]
        public void CreateFileName()
        {
            var fileName = FileNameFactory.DefaultFileName();

            Assert.Equal(FileNameFactory.DefaultName, fileName.Name);
            Assert.Equal(FileNameFactory.DefaultExtension, fileName.Extension);
            Assert.Equal(FileNameFactory.DefaultName + "." + FileNameFactory.DefaultExtension, fileName.ToString());
        }

        [Fact]
        public void ValidFileNames()
        {
            var fileName = new FileName("test.pdf");
            var fileName2 = new FileName("c:\\test.pdf");
            var fileName3 = new FileName("MT.Test.csproj");
            var fileName4 = new FileName("c:\\test\\MT.Test.csproj");
            var fileName5 = new FileName("http://www.test.com/test.pdf");

            Assert.Equal("test.pdf", fileName.ToString());
            Assert.Equal("test.pdf", fileName2.ToString());
            Assert.Equal("MT.Test.csproj", fileName3.ToString());
            Assert.Equal("MT.Test.csproj", fileName4.ToString());
            Assert.Equal("test.pdf", fileName5.ToString());
        }
    }
}