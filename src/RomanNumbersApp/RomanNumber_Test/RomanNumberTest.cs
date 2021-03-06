using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomanHelper.Extension;

namespace RomanNumber_Test
{
    [TestClass]
    public class RomanNumberTest
    {
        [TestMethod]
        public void Test_ConvertToInteger()
        {
            /// Arrange
            int expected = 14;
            string romanNumber = "XV";

            /// Act
            var actual = romanNumber.ConvertToInteger();

            /// Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
