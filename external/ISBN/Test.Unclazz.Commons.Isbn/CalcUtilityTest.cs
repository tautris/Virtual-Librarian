using Unclazz.Commons.Isbn;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test.Unclazz.Commons.Isbn
{
    [TestFixture]
    public class CalcUtilityTest
    {
        [TestCase("ISBN4-88380-478-X", ExpectedResult = 'X')]
        [TestCase("067976127", ExpectedResult = '6')]
        [TestCase("493866123", ExpectedResult = '3')]
        public char Modulus11_WhenStringHasValidLength_ReturnsCheckDigit(string chars)
        {
            // Arrange
            var digits = Regex.Replace(chars, "[^0-9]+", string.Empty).Take(9);
            // Act
            // Assert
            return CalcUtility.CheckDigitForIsbn10(digits);
        }

        [TestCase("ISBN: 978-067976127", ExpectedResult = '3')]
        [TestCase("978-493866123", ExpectedResult = '6')]
        [TestCase("491234567890", ExpectedResult = '4')]
        public char Modulus10_WhenStringHasValidLength_ReturnsCheckDigit(string chars)
        {
            // Arrange
            var digits = Regex.Replace(chars, "[^0-9]+", string.Empty).Take(12);
            // Act
            // Assert
            return CalcUtility.CheckDigitForIsbn13(digits);
        }

        [TestCase("4883804789")]
        [TestCase("06797612")]
        [TestCase("")]
        public void Modulus11_WhenStringHasInvalidLength_ThrowsException(string chars)
        {
            // Arrange
            // Act
            // Assert
            Assert.That(() => CalcUtility.CheckDigitForIsbn10(chars), Throws.InstanceOf<ArgumentException>());
        }

        [TestCase("0978067976127")]
        [TestCase("97849386612")]
        [TestCase("")]
        public void Modulus10_WhenStringHasInvalidLength_ThrowsException(string chars)
        {
            // Arrange
            // Act
            // Assert
            Assert.That(() => CalcUtility.CheckDigitForIsbn13(chars), Throws.InstanceOf<ArgumentException>());
        }
    }
}
