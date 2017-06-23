using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabysitterKata
{
    /// <summary>
    /// Stores a babysitter's start and end time based on the 24-hour clock.
    /// </summary>
    public class BabysitterTimeCard
    {

        private TwentyFourHourTime EARLIEST_START_TIME = new TwentyFourHourTime(17, 0);
        private TwentyFourHourTime LATEST_END_TIME = new TwentyFourHourTime(4, 0);
        private TwentyFourHourTime ONE_MINUTE_TO_MIDNIGHT = new TwentyFourHourTime(23, 59);
        private TwentyFourHourTime MIDNIGHT = new TwentyFourHourTime(0, 0);
        private TIME_OF_DAY _startTimePeriod;
		private TIME_OF_DAY _bedTimePeriod;
        private TIME_OF_DAY _endTimePeriod;
        private TwentyFourHourTime _startTime;
        private TwentyFourHourTime _endTime;
        private TwentyFourHourTime _bedTime;

        /// <summary>
        /// The time the babysitting shift began.
        /// Valid Range: 17:00 - 23:59
        /// </summary>
        public TwentyFourHourTime StartTime { get { return _startTime; } }

        /// <summary>
        /// The time the babysitting shift ended.
        /// Valid Range: 17:00 - 04:00
        /// Must come after <see cref="StartTime"/>
        /// </summary>
        public TwentyFourHourTime EndTime { get { return _endTime; } }

		/// <summary>
		/// The time that the child went to bed.
		/// Valid Range: 17:00 - 23:59
		/// Must fall between <see cref="StartTime"/> and <see cref="MIDNIGHT"/>
		/// </summary>
		public TwentyFourHourTime BedTime { get { return _bedTime; } }

        /// <summary>
        /// This constructor for a TimeCard must include a start and an end time.
        /// </summary>
        /// <param name="startTime">Time shift began, valid range: 17:00 - 04:00</param>
        /// <param name="endTime">Time shift ended, valid range: 17:00 - 04:00, must come after <see cref="StartTime"/></param>
        public BabysitterTimeCard(TwentyFourHourTime startTime, TwentyFourHourTime endTime)
        {
            if (startTimeIsValid(startTime))
                _startTime = startTime;

            _startTimePeriod = enumerateStartTimePeriod();

            if (endTimeIsValid(endTime))
                _endTime = endTime;
        }

        /// <summary>
        /// This constructor for a TimeCard must include a start time, an end time, and a bed time.
        /// </summary>
        /// <param name="startTime">Time shift began, valid range: 17:00 - 4:00</param>
        /// <param name="endTime">Time shift ended, valid range: 17:00 - 04:00, must come after <see cref="StartTime"/></param>
        /// <param name="bedTime">Time child went to bed, valid range: 17:00 - 04:00, must come between <see cref="StartTime"/> and <see cref="EndTime"/></param>
        public BabysitterTimeCard(TwentyFourHourTime startTime, TwentyFourHourTime endTime, TwentyFourHourTime bedTime)
        {
            if (startTimeIsValid(startTime))
                _startTime = startTime;

            _startTimePeriod = enumerateStartTimePeriod();

            if (endTimeIsValid(endTime))
                _endTime = endTime;

            _endTimePeriod = enumerateEndTimePeriod();
            
            if (bedTimeIsValid(bedTime))
                _bedTime = bedTime;

			_bedTimePeriod = enumerateBedTimePeriod();
        }

        /// <summary>
        /// Calculate total time of a shift.
        /// </summary>
        /// <returns><see cref="double"/> representing the total time of a shift in hours</returns>
        public double CalculateTotalTime()
        {
            double hours = _endTime.Hours - _startTime.Hours;
            double minutes = _endTime.Minutes - _startTime.Minutes;
            return hours + minutes / 60;
        }

        /// <summary>
        /// Calculates hours worked before bedtime.
        /// </summary>
        /// <returns><see cref="int"/> representing the hours worked before bed, to the nearest hour</returns>
        public int CalculateHoursBeforeBedtime()
        {
            switch (_startTimePeriod)
            {
                case TIME_OF_DAY.AM:
                    return calculateHoursBeforeBedtime_MorningStart();
                    break;
                default:
                    return calculateHoursBeforeBedtime_EveningStart();
                    break;
            }
        }

        /// <summary>
        /// Calculates hours worked before midnight, after bedtime.
        /// </summary>
        /// <returns><see cref="int"/> representing the hours worked before bed, to the nearest hour</returns>
		public int CalculateHoursBetweenBedtimeAndMidnight()
		{
			switch (_bedTimePeriod)
			{
				case TIME_OF_DAY.AM:
					return 0;
					break;
				default:
                    if(babysitterWorksPastMidnight())
					    return (int) Math.Round(ONE_MINUTE_TO_MIDNIGHT.Minus(_bedTime) + (1.0/60.0));
                    return (int) Math.Round(_endTime.Minus(_bedTime));
					break;
			}
		}	

        /// <summary>
        /// Calculates hours worked afer midnight.
        /// </summary>
        /// <returns><see cref="int"/> representing the hours worked after midnight, to the nearest hour</returns>
        public int CalculateHoursAfterMidnight()
        {
            switch (_endTimePeriod)
            {
                case TIME_OF_DAY.PM:
                    return 0;
                    break;
                default:
                    return (int)Math.Round(_endTime.Minus(MIDNIGHT));
            }
        }

        private bool babysitterWorksPastMidnight()
        {
            switch (_endTimePeriod)
            {
                case TIME_OF_DAY.AM:
                    return true;
                    break;
                default:
                    return false;
                    break;
            }
        }

        private int calculateHoursBeforeBedtime_MorningStart()
        {
            return (int) Math.Round(_bedTime.Minus(_startTime));
        }

        private int calculateHoursBeforeBedtime_EveningStart()
        {
            if (_bedTime.CompareTo(EARLIEST_START_TIME) < 0)
                return (int) Math.Round(ONE_MINUTE_TO_MIDNIGHT.Minus(_startTime) + _bedTime.Minus(MIDNIGHT));
            return (int) Math.Round(_bedTime.Minus(_startTime));
        }

        private bool startTimeIsValid(TwentyFourHourTime startTime)
        {
            if (startTime.CompareTo(EARLIEST_START_TIME) >= 0)
                return true;
            throw new ArgumentOutOfRangeException("Babysitter cannot start work before 5:00PM or after Midnight");
            return false;
        }

        private bool endTimeIsValid(TwentyFourHourTime endTime)
        {
            if ((endTime.CompareTo(EARLIEST_START_TIME) >= 0 || endTime.CompareTo(LATEST_END_TIME) <= 0) && startTimePreceedsEndTime(endTime))
                return true;
            throw new ArgumentOutOfRangeException("Babysitters cannot work after 4:00AM");
            return false;
        }

        private bool startTimePreceedsEndTime(TwentyFourHourTime endTime)
        {
            switch (_startTimePeriod)
            {
                case TIME_OF_DAY.AM:
                    if (endTime.CompareTo(_startTime) >= 0 && endTime.CompareTo(LATEST_END_TIME) <=0)
                        return true;
                    throw new ArgumentException("End time must come after start time");
                    return false;
                    break;
                default:
                    if (endTime.CompareTo(_startTime) >= 0 || endTime.CompareTo(LATEST_END_TIME) <= 0)
                        return true;
                    throw new ArgumentException("End time must come after start time");
                    return false;
                    break;
            }
        }

        private bool bedTimeIsValid(TwentyFourHourTime bedTime)
        {
            if (startTimePreceedsBedTime(bedTime) && bedTimePreceedsEndTime(bedTime))
                return true;
            return false;
        }

        private bool startTimePreceedsBedTime(TwentyFourHourTime bedTime)
        {
            if (bedTime.CompareTo(StartTime) >= 0)
                return true;
            throw new ArgumentException("Bed time must come after start time");
            return false;
        }

        private bool bedTimePreceedsEndTime(TwentyFourHourTime bedTime)
        {
            switch(_endTimePeriod)
            {
                case TIME_OF_DAY.PM:
                    if (bedTime.CompareTo(StartTime) >= 0 && bedTime.CompareTo(EndTime) <= 0)
                        return true;
                    throw new ArgumentException("Bed time must come before end time");
                    return false;
                    break;
                default:
                    if (bedTime.CompareTo(EndTime) <= 0 || bedTime.CompareTo(EARLIEST_START_TIME) >= 0)
                        return true;
                    throw new ArgumentException("Bed time must come before end time");
                    return false;
                    break;
            }
            
        }

        private TIME_OF_DAY enumerateStartTimePeriod()
        {
            if (StartTime.CompareTo(EARLIEST_START_TIME) >= 0)
                return TIME_OF_DAY.PM;
            return TIME_OF_DAY.AM;
        }

		private TIME_OF_DAY enumerateBedTimePeriod()
		{
			if (BedTime.CompareTo(EARLIEST_START_TIME) >= 0)
				return TIME_OF_DAY.PM;
			return TIME_OF_DAY.AM;
		}

        private TIME_OF_DAY enumerateEndTimePeriod()
        {
            if (EndTime.CompareTo(EARLIEST_START_TIME) >= 0)
                return TIME_OF_DAY.PM;
            return TIME_OF_DAY.AM;
        }
    }
}
