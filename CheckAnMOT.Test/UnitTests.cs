using System.Runtime.CompilerServices;
using CheckAnMOT.Core.Helpers;

namespace CheckAnMOT.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("$P23-@DO")]
        [TestCase("WP73 MKN")]
        [TestCase("WP23-ADO")]
        public void InputRegPlate_WithSpecialCharsOrSpaces_ReturnFalse(string input)
        {
            var result = Helpers.ValidRegPlate(input);

            Assert.That(result, Is.False, "Special characters should not occur in number plate");
        }
    }
}