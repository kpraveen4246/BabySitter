using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BabysitterKata;

namespace BabysitterKata.Tests
{
	class BabysitterTimeCard_Tests
	{
		[TestCase(19, 15, 22, 32)]
		[TestCase(20, 52, 03, 15)]
		[TestCase(17, 00, 04, 00)]
		[TestCase(17, 23, 3, 19)]
		[TestCase(23, 42, 2, 4)]
		public void BabysitterTimeCard_GivenTwoValidTime_CalculatesTotalTime(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes)
		{
			TwentyFourHourTime startTime = new TwentyFourHourTime(startTimeHours, startTimeMinutes);
			TwentyFourHourTime endTime = new TwentyFourHourTime(endTimeHours, endTimeMinutes);

			BabysitterTimeCard timeCard = new BabysitterTimeCard(startTime, endTime);

			Assert.AreEqual((endTime.Hours - startTime.Hours) + (((double)endTime.Minutes - (double)startTime.Minutes) / 60), timeCard.CalculateTotalTime());
		}

		[TestCase(14, 32, 22, 15)]
		[TestCase(5, 14, 23, 14)]
		[TestCase(12, 0, 4, 0)]
		[TestCase(11, 22, 7, 19)]
		[TestCase(8, 48, 19, 12)]
		public void BabySitterTimeCard_GivenStartTimeBefore5PM_ThrowsArgumentOutOfRangeException(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes)
		{
			TwentyFourHourTime startTime = new TwentyFourHourTime(startTimeHours, startTimeMinutes);
			TwentyFourHourTime endTime = new TwentyFourHourTime(endTimeHours, endTimeMinutes);

			Assert.Throws<ArgumentOutOfRangeException>(delegate { new BabysitterTimeCard(startTime, endTime); });
		}

        [TestCase(01, 15, 02, 30)]
        [TestCase(02, 30, 04, 00)]
        [TestCase(00, 01, 03, 30)]
        [TestCase(03, 01, 03, 02)]
        public void BabysitterTimeCard_GivenStartTimeAfterMidnight_ThrowsArguementOutOfRangeException(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes)
        {
            TwentyFourHourTime startTime = new TwentyFourHourTime(startTimeHours, startTimeMinutes);
            TwentyFourHourTime endTime = new TwentyFourHourTime(endTimeHours, endTimeMinutes);

            Assert.Throws<ArgumentOutOfRangeException>(delegate { new BabysitterTimeCard(startTime, endTime); });
        }

		[TestCase(17, 22, 06, 15)]
		[TestCase(19, 45, 07, 36)]
		[TestCase(22, 0, 4, 01)]
		[TestCase(0, 0, 6, 55)]
		[TestCase(17, 16, 13, 12)]
		public void BabySitterTimeCard_GivenEndTimeAfter4AM_ThrowsArgumentOutOfRangeException(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes)
		{
			TwentyFourHourTime startTime = new TwentyFourHourTime(startTimeHours, startTimeMinutes);
			TwentyFourHourTime endTime = new TwentyFourHourTime(endTimeHours, endTimeMinutes);

			Assert.Throws<ArgumentOutOfRangeException>(delegate { new BabysitterTimeCard(startTime, endTime); });
		}

		[TestCase(17, 25, 17, 0)]
		[TestCase(22, 32, 19, 15)]
        [TestCase(23, 39, 17, 15)]
        [TestCase(19, 00, 18, 00)]
        [TestCase(20, 00, 17, 00)]
		public void BabySittertimeCard_GivenEndTimeBeforeStartTime_ThrowsArgumentException(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes)
		{
			TwentyFourHourTime startTime = new TwentyFourHourTime(startTimeHours, startTimeMinutes);
			TwentyFourHourTime endTime = new TwentyFourHourTime(endTimeHours, endTimeMinutes);

			Assert.Throws<ArgumentException>(delegate { new BabysitterTimeCard(startTime, endTime); });
		}

        [TestCase(18, 22, 22, 45, 17, 15)]
        [TestCase(19, 30, 23, 59, 18, 30)]
        [TestCase(20, 00, 21, 00, 19, 00)]
        [TestCase(17, 15, 22, 15, 17, 05)]
        public void BabysitterTimeCard_GivenBedTimeBeforeStartTime_ThrowsArgumentException(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes, int bedTimeHours, int bedTimeMinutes)
        {
            TwentyFourHourTime startTime = new TwentyFourHourTime(startTimeHours, startTimeMinutes);
            TwentyFourHourTime endTime = new TwentyFourHourTime(endTimeHours, endTimeMinutes);
            TwentyFourHourTime bedTime = new TwentyFourHourTime(bedTimeHours, bedTimeMinutes);

            Assert.Throws<ArgumentException>(delegate { new BabysitterTimeCard(startTime, endTime, bedTime); });
        }

        [TestCase(17, 00, 18, 00, 21, 30)]
        [TestCase(19, 23, 22, 45, 02, 30)]
        [TestCase(20, 15, 02, 30, 03, 45)]
        [TestCase(21, 45, 01, 15, 02, 00)]
        public void BabysitterTimeCard_GivenBedTimeAfterEndTime_ThrowsArgumentException(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes, int bedTimeHours, int bedTimeMinutes)
        {
            TwentyFourHourTime startTime = new TwentyFourHourTime(startTimeHours, startTimeMinutes);
            TwentyFourHourTime endTime = new TwentyFourHourTime(endTimeHours, endTimeMinutes);
            TwentyFourHourTime bedTime = new TwentyFourHourTime(bedTimeHours, bedTimeMinutes);

            Assert.Throws<ArgumentException>(delegate { new BabysitterTimeCard(startTime, endTime, bedTime); });
        }

        [TestCase(17, 00, 04, 00, 01, 15)]
        [TestCase(18, 35, 02, 15, 02, 15)]
        [TestCase(23, 30, 01, 15, 01, 00)]
        [TestCase(20, 00, 00, 03, 00, 01)]
        public void BabysitterTimeCard_GivenBedTimeAfterMidnight_ThrowsArgumentException(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes, int bedTimeHours, int bedTimeMinutes)
        {
            TwentyFourHourTime startTime = new TwentyFourHourTime(startTimeHours, startTimeMinutes);
            TwentyFourHourTime endTime = new TwentyFourHourTime(endTimeHours, endTimeMinutes);
            TwentyFourHourTime bedTime = new TwentyFourHourTime(bedTimeHours, bedTimeMinutes);

            Assert.Throws<ArgumentException>(delegate { new BabysitterTimeCard(startTime, endTime, bedTime); });
        }

        [TestCase(17, 15, 23, 45, 22, 0, 5)]
		[TestCase(18, 23, 01, 52, 23, 30, 5)]
		[TestCase(21, 05, 03, 35, 22, 35, 2)]
		[TestCase(17, 01, 22, 30, 17, 25, 0)]
		[TestCase(18, 20, 23, 32, 22, 30, 4)]
		public void BabySitterTimeCard_GivenValidStartTimeAndBedTime_ReturnHoursBeforeBedtime(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes, int bedTimeHours, int bedTimeMinutes, int hoursBeforeBedTime)
		{
            var timeCard = initializeTimeCard(startTimeHours, startTimeMinutes, endTimeHours, endTimeMinutes, bedTimeHours, bedTimeMinutes);

            Assert.AreEqual(hoursBeforeBedTime, timeCard.CalculateHoursBeforeBedtime());
		}

		[TestCase(17, 16, 01, 05, 22, 00, 2)]
        [TestCase(17, 00, 22, 45, 21, 16, 1)]
        [TestCase(18, 52, 23, 58, 22, 45, 1)]
        [TestCase(22, 15, 02, 30, 22, 30, 2)]
        [TestCase(17, 52, 01, 40, 21, 30, 2)]
        [TestCase(18, 12, 21, 32, 18, 52, 3)]
		public void BabysitterTimeCard_GivenValidStartEndAndBedTime_ReturnHoursBetweenBedtimeAndMidnight(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes, int bedTimeHours, int bedTimeMinutes, int hoursBetweenBedtimeAndMidnight)
		{
            var timeCard = initializeTimeCard(startTimeHours, startTimeMinutes, endTimeHours, endTimeMinutes, bedTimeHours, bedTimeMinutes);

			Assert.AreEqual(hoursBetweenBedtimeAndMidnight, timeCard.CalculateHoursBetweenBedtimeAndMidnight());
		}

        [TestCase(17, 45, 03, 33, 22, 45, 4)]
        [TestCase(18, 45, 23, 11, 22, 50, 0)]
        [TestCase(17, 00, 01, 18, 23, 15, 1)]
        [TestCase(19, 15, 02, 31, 21, 43, 3)]
        public void BabysitterTimeCard_GivenValidBabysitterTimeCard_ReturnHoursAfterMidnight(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes, int bedTimeHours, int bedTimeMinutes, int hoursAfterMidnight)
        {
            var timeCard = initializeTimeCard(startTimeHours, startTimeMinutes, endTimeHours, endTimeMinutes, bedTimeHours, bedTimeMinutes);

            Assert.AreEqual(hoursAfterMidnight, timeCard.CalculateHoursAfterMidnight());
        }

        private BabysitterTimeCard initializeTimeCard(int startTimeHours, int startTimeMinutes, int endTimeHours, int endTimeMinutes, int bedTimeHours, int bedTimeMinutes)
        {
            var startTime = new TwentyFourHourTime(startTimeHours, startTimeMinutes);
            var endTime = new TwentyFourHourTime(endTimeHours, endTimeMinutes);
            var bedTime = new TwentyFourHourTime(bedTimeHours, bedTimeMinutes);

            return new BabysitterTimeCard(startTime, endTime, bedTime);
        }
	}
}
