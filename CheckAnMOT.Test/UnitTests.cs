using System.Runtime.CompilerServices;
using CheckAnMOT.Core.Helpers;
using NuGet.Frameworks;

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

        [TestCase("OFZ2807")]
        [TestCase("M823FTT")]
        public void InputValidRegPlate_WithUnusualFormat_ReturnTrue(string input)
        {
            var result = Helpers.ValidRegPlate(input);
            Assert.That(result, Is.True, "Not all plates are AB12 CDE");
        }

        [TestCase("OFZ 2807")]
        [TestCase("M823 FTT")]
        [TestCase("WP73 MKN")]
        public void FormatInvalidWhitespace_BeforeCheckingPlateIsValid_ReturnTrue(string input)
        {
            input = Helpers.RemoveUserWhiteSpace(input);
            var result = Helpers.ValidRegPlate(input);

            Assert.That(result, Is.True, "User input whitespace must be stripped");
        }
    }
}