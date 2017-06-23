using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BabysitterKata;

namespace BabysitterKata.Tests
{
    public class TwentyFourHourTime_Tests
    {
        [TestCase(18, 13)]
        [TestCase(20, 57)]
        [TestCase(4, 15)]
        [TestCase(15, 0)]
        [TestCase(23, 32)]
        public void TwentyFourHourTime_GivenValidTime_HoursInitializeProperly(int hours, int minutes)
        {
            TwentyFourHourTime testTime = new TwentyFourHourTime(hours, minutes);
            Assert.That(testTime.Hours, Is.EqualTo(hours));
        }

        [TestCase(14, 32)]
        [TestCase(12, 13)]
        [TestCase(5, 42)]
        [TestCase(17, 0)]
        [TestCase(3, 36)]
        public void TwentyFourHourTime_GivenValidTime_MinutesInitializeProperly(int hours, int minutes)
        {
            TwentyFourHourTime testTime = new TwentyFourHourTime(hours, minutes);
            Assert.That(testTime.Minutes, Is.EqualTo(minutes));
        }

        [TestCase(24, 14)]
        [TestCase(52, 36)]
        [TestCase(0, 72)]
        [TestCase(3, 60)]
        [TestCase(-1, 42)]
        [TestCase(3, -14)]
        [TestCase(9, 940)]
        public void TwentyFourHourTime_GivenInvalidTime_ThrowsArgumentOutOfRangeException(int hours, int minutes)
        {
            Assert.Throws<ArgumentOutOfRangeException>(delegate { new TwentyFourHourTime(hours, minutes); });
        }
    }
}
