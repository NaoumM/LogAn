using System;
using LogAn;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {

        private LogAnalyzer _analyzer;

        [SetUp]
        public void SetUp()
        {
            _analyzer = new LogAnalyzer();
        }

        [TearDown]
        public void TearDown()
        {
        }

        private LogAnalyzer MakeAnalyzer()
        {
            return new LogAnalyzer();
        }

        [TestCase("badName.foo", false)]
        [TestCase("goodName.slf", true)]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid(string filename, bool expected)
        {
            LogAnalyzer la = MakeAnalyzer();

            la.IsValidLogFileName(filename);

            Assert.AreEqual(expected, la.WasLastFileNameValid);
        }

        [Test]
        [Ignore("Failing")]
        [Category("Failed Test")]
        public void IsValidFileName_EmptyFileName_Throws()
        {
            LogAnalyzer la = MakeAnalyzer();

            var ex = Assert.Catch<Exception>(() => la.IsValidLogFileName(" "));

            StringAssert.Contains("filename has to be provided", ex.Message);
        }

        [Test]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            bool result = _analyzer.IsValidLogFileName("fileWithBadExtension.foo");

            Assert.False(result);
        }

        [TestCase("fileWithGoodExtension.slf")]
        [TestCase("fileWithGoodExtension.SLF")]
        public void IsValidLogFileName_ValidExtensions_ReturnsTrue(string file)
        {
            bool result = _analyzer.IsValidLogFileName(file);

            Assert.True(result);
        }

    }
}