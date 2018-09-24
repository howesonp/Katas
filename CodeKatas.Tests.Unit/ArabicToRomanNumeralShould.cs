using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace CodeKatas.Tests.Unit
{
    [TestFixture]
    public class ArabicToRomanNumeralShould
    {
        [TestCase(1, "I")]
        [TestCase(2, "II")]
        [TestCase(3, "III")]
        [TestCase(4, "IV")]
        [TestCase(5, "V")]
        [TestCase(6, "VI")]
        [TestCase(7, "VII")]
        [TestCase(8, "VIII")]
        [TestCase(9, "IX")]
        [TestCase(10, "X")]
        [TestCase(14, "XIV")]
        [TestCase(41, "XLI")]
        [TestCase(1234, "MCCXXXIV")]
        [TestCase(2018, "MMXVIII")]
        public void ReturnRomanNumeral_WhenConverting_GivenArabicNumber(int arabicNumber, string expectedRomanNumeral)
        {
            var arabicToRomanNumeral = new ArabicToRomanNumeral();

            var actualRomanNumeral = arabicToRomanNumeral.Convert(arabicNumber);

            actualRomanNumeral.Should().Be(expectedRomanNumeral);
        }

        [Test]
        public void ThrowException_WhenConverting_GivenArabicNumberWhichIsLessThanOne()
        {
            var arabicToRomanNumeral = new ArabicToRomanNumeral();

            Action actualRomanNumeral =() =>  arabicToRomanNumeral.Convert(0);

            actualRomanNumeral.Should().Throw<Exception>();
        }
    }
}
