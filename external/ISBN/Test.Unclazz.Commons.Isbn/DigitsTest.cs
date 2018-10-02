using Unclazz.Commons.Isbn;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Unclazz.Commons.Isbn
{
    [TestFixture]
    public class DigitsTest
    {
        [Test]
        public void Of_String_WhenStringWithLeadingZeros_ReturnsDigitsWithLeadingZeros()
        {
            // Arrange
            // Act
            var d01 = Digits.Of("01");
            var d001 = Digits.Of("001");
            var d0000 = Digits.Of("0000");

            // Assert
            Assert.That(d01.StringValue, Does.StartWith("0"));
            Assert.That(d01.IntValue, Is.EqualTo(1));
            Assert.That(d01.Length, Is.EqualTo(2));

            Assert.That(d001.StringValue, Does.StartWith("00"));
            Assert.That(d001.IntValue, Is.EqualTo(1));
            Assert.That(d001.Length, Is.EqualTo(3));

            Assert.That(d0000.StringValue, Does.StartWith("0000"));
            Assert.That(d0000.IntValue, Is.EqualTo(0));
            Assert.That(d0000.Length, Is.EqualTo(4));
        }

        [Test]
        public void Of_String_WhenStringIsEmpty_ThrowsException()
        {
            // Arrange
            // Act
            // Assert
            Assert.That(() => Digits.Of(string.Empty), Throws.InstanceOf<ArgumentException>());
            Assert.That(() => Digits.Of(null), Throws.InstanceOf<ArgumentNullException>());
        }

        [Test]
        public void Of_String_WhenStringIncludesNonDigit_ThrowsException()
        {
            // Arrange
            // Act
            // Assert
            Assert.That(() => Digits.Of("a"), Throws.InstanceOf<ArgumentException>());
            Assert.That(() => Digits.Of("-"), Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        public void Of_int_WhenIntIsZeroOrPositive_ReturnsDigits()
        {
            // Arrange
            // Act
            var d0 = Digits.Of(0);
            var d1 = Digits.Of(1);
            var d10 = Digits.Of(10);

            // Assert
            Assert.That(d0.StringValue, Does.StartWith("0"));
            Assert.That(d0.IntValue, Is.EqualTo(0));
            Assert.That(d0.Length, Is.EqualTo(1));

            Assert.That(d1.StringValue, Does.StartWith("1"));
            Assert.That(d1.IntValue, Is.EqualTo(1));
            Assert.That(d1.Length, Is.EqualTo(1));

            Assert.That(d10.StringValue, Does.StartWith("10"));
            Assert.That(d10.IntValue, Is.EqualTo(10));
            Assert.That(d10.Length, Is.EqualTo(2));
        }

        [Test]
        public void Of_int_WhenIntIsNegative_ThrowsException()
        {
            // Arrange
            // Act
            // Assert
            Assert.That(() => Digits.Of(-1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => Digits.Of(-2), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Of_int_int_When2ndIntIsPositive_ReturnsDigits()
        {
            // Arrange
            // Act
            var d0 = Digits.Of(0, 2);
            var d1 = Digits.Of(1, 2);
            var d10 = Digits.Of(10, 3);

            // Assert
            Assert.That(d0.StringValue, Does.StartWith("00"));
            Assert.That(d0.IntValue, Is.EqualTo(0));
            Assert.That(d0.Length, Is.EqualTo(2));

            Assert.That(d1.StringValue, Does.StartWith("01"));
            Assert.That(d1.IntValue, Is.EqualTo(1));
            Assert.That(d1.Length, Is.EqualTo(2));

            Assert.That(d10.StringValue, Does.StartWith("010"));
            Assert.That(d10.IntValue, Is.EqualTo(10));
            Assert.That(d10.Length, Is.EqualTo(3));
        }

        [Test]
        public void Of_int_int_When2ndIntIsZeroOrNegative_ThrowsException()
        {
            // Arrange
            // Act
            // Assert
            Assert.That(() => Digits.Of(0, 0), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => Digits.Of(0, -1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => Digits.Of(1, 0), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => Digits.Of(1, -1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => Digits.Of(10, 0), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => Digits.Of(10, -1), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Equals_ComparesTwoInstances_BasedOnThoseStringValue()
        {
            // Arrange
            var d0 = Digits.Of(0);
            var d1 = Digits.Of(0);
            var d2 = Digits.Of("0");
            var d3 = Digits.Of("00");

            // Act
            // Assert
            Assert.That(d0.Equals(d0), Is.True);
            Assert.That(d0.Equals(d1), Is.True);
            Assert.That(d1.Equals(d2), Is.True);
            Assert.That(d2.Equals(d0), Is.True);
            Assert.That(d2.Equals(d3), Is.False);
            Assert.That(d2.IntValue.Equals(d3.IntValue), Is.True);
        }

        [TestCase("0", "0", ExpectedResult = true)]
        [TestCase("00", "0", ExpectedResult = false)]
        [TestCase("0", "00", ExpectedResult = false)]
        [TestCase(null, "0", ExpectedResult = false)]
        [TestCase("0", null, ExpectedResult = false)]
        public bool OperatorEqualEqual_ComparesTwoInstances_BasedOnThoseStringValue(string v0, string v1)
        {
            return (v0 == null ? null : Digits.Of(v0))
                == (v1 == null ? null : Digits.Of(v1));
        }
    }
}
