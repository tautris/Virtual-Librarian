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
    public class RegistrationGroupsTest
    {
        [Test]
        public void LoadGroups_WhenRangeMessageXmlExists_ReturnsGroups()
        {
            // Arrange
            // Act
            var gs = RegistrationGroups.LoadGroups();
            var g978_0 = gs.First();
            var g978_0_Rules = g978_0.Rules.ToList();
            var g978_605 = gs.First(g => g.Prefix == "978-605");
            var g978_605_Rules = g978_605.Rules.ToList();

            // Assert
            Assert.That(g978_0.Prefix, Is.EqualTo("978-0"));
            Assert.That(g978_0.Agency, Is.EqualTo("English language"));
            Assert.That(g978_0_Rules[0].RangeStart, Is.EqualTo("00"));
            Assert.That(g978_0_Rules[0].RangeEnd, Is.EqualTo("19"));
            Assert.That(g978_0_Rules[0].Length, Is.EqualTo(2));
            Assert.That(g978_0_Rules[9].RangeStart, Is.EqualTo("9500000"));
            Assert.That(g978_0_Rules[9].RangeEnd, Is.EqualTo("9999999"));
            Assert.That(g978_0_Rules[9].Length, Is.EqualTo(7));
            Assert.That(g978_0_Rules.Count(), Is.EqualTo(10));

            Assert.That(g978_605_Rules[0].RangeStart, Is.EqualTo(""));
            Assert.That(g978_605_Rules[0].RangeEnd, Is.EqualTo(""));
            Assert.That(g978_605_Rules[0].Length, Is.EqualTo(0));
        }
    }
}
