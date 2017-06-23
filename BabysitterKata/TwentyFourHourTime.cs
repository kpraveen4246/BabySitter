using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabysitterKata
{
    /// <summary>
    /// A representation of time in 24 hour clock format
    /// Valid times range from 0000 to 2359.
    /// </summary>
    public class TwentyFourHourTime : IComparable, IComparable<TwentyFourHourTime>, IEquatable<TwentyFourHourTime>
    {
        private const int MIN_HOURS_VALUE = 0;
        private const int MIN_MINUTES_VALUE = 0;
        private const int MAX_HOURS_VALUE = 23;
        private const int MAX_MINUTES_VALUE = 59;
        private int _hours;
        private int _minutes;

        /// <summary>
        /// The number of hours since midnight.
        /// </summary>
        public int Hours { get{ return _hours; } }

        /// <summary>
        /// The number of minutes since the last hour.
        /// </summary>
        public int Minutes { get { return _minutes; } }

        /// <summary>
        /// Creates a <see cref="TwentyFourHourTime"/>.
        /// </summary>
        /// <param name="hours">
        /// The number of hours since midnight.
        /// Valid Range: 0-23
        /// </param>
        /// <param name="minutes">
        /// The number of minutes since the last turn of the hour.
        /// Valid Range: 0-59
        /// </param>
        public TwentyFourHourTime(int hours, int minutes)
        {
            if(ValidateMinuteValue(minutes) && ValidateHourValue(hours))
            {
                _hours = hours;
                _minutes = minutes;
            }else
            {
                throw new ArgumentOutOfRangeException("Arguments are outside acceptable range.");
            }
        }

        private bool ValidateMinuteValue(int minutes)
        {
            if (minutes <= MAX_MINUTES_VALUE && minutes >= MIN_MINUTES_VALUE)
                return true;
            return false;
        }

        private bool ValidateHourValue(int hours)
        {
            if (hours <= MAX_HOURS_VALUE && hours >= MIN_HOURS_VALUE)
                return true;
            return false;
        }

        /// <summary>
        /// Compares this instance to a specific <see cref="Object"/>t and returns an indication of their relative values.
        /// </summary>
        /// <param name="obj">Instance of any subclass of <see cref="Object"/> to be compared.</param>
        /// <returns>
        /// Relative indicator of value as follows:
        /// -1: This instance is before obj chronologically
        /// 0: This instance is the same point in time as obj
        /// 1: This instance is after obj chronologically
        /// </returns>
        public int CompareTo(object obj)
        {
            obj = (TwentyFourHourTime)obj;
            if (obj != null)
                return this.CompareTo(obj);
            
            //Objects of TwentyFourHourTime should always be greater than other objects
            return 1;
        }

        /// <summary>
        /// Compares this instance to a specific <see cref="TwentyFourHourTime"/> and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">Instance of <see cref="TwentyFourHourTime"/> to be compared.</param>
        /// <returns>
        /// -1: This instance is before obj chronologically
        /// 0: This instance is the same point in time as obj
        /// 1: This instance is after obj chronologically
        /// </returns>
        public int CompareTo(TwentyFourHourTime other)
        {
            if (this.Hours == other.Hours)
                return this.Minutes.CompareTo(other.Minutes);
            return this.Hours.CompareTo(other.Hours);
        }

        /// <summary>
        /// Indicates equality between this and another specific <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">Instance of any subclass of <see cref="Object"/> to be compared.</param>
        /// <returns>True if equal, else false</returns>
        public bool Equals(object obj)
        {
            obj = (TwentyFourHourTime)obj;
            if (obj != null)
                return this.Equals(obj);

            //Objects of TwentyFourHourTime cannot be equal to objects of other classes.
            return false;
        }

        /// <summary>
        /// Indicates equality between this and another specific <see cref="TwentyFourHourTime"/>
        /// </summary>
        /// <param name="other">Instance of <see cref="TwentyFourHourTime"/> to be compared.</param>
        /// <returns>True if equal, else false</returns>
        public bool Equals(TwentyFourHourTime other)
        {
            return this.Hours.Equals(other.Hours) && this.Minutes.Equals(other.Minutes);
        }

        /// <summary>
        /// Calculates the difference between this and another <see cref="TwentyFourHourTime"/>
        /// </summary>
        /// <param name="other"><see cref="TwentyFourHourTime"/> to subtract</param>
        /// <returns>Result of subtraction</returns>
        public double Minus(TwentyFourHourTime other)
        {
            int otherHours = other.Hours;
            int otherMinutes = other.Minutes;

            if (other.Hours > this.Hours)
            {
                otherHours += 24;
            }
            if (other.Minutes > this.Minutes)
            {
                otherMinutes += 60;
                otherHours--;
            }

            return subtract(otherHours, otherMinutes);
        }

        private double subtract(int otherHours, int otherMinutes)
        {
            double hours = this.Hours - otherHours;
            double minutes = this.Minutes - otherMinutes;
            return hours + minutes / 60;
        }

        /// <summary>
        /// Calculates the sum of this and another <see cref="TwentyFourHourTime"/>
        /// </summary>
        /// <param name="other"><see cref="TwentyFourHourTime"/> to add</param>
        /// <returns>Result of addition</returns>
        public double Plus(TwentyFourHourTime other)
        {
            double hours = this.Hours + other.Hours;
            double minutes = this.Minutes + other.Minutes;

            if (minutes >= 60)
            {
                minutes -= 60;
                hours++;
            }

            if (hours >= 24)
            {
                hours -= 24;
            }

            return hours + minutes / 60;
        }
    }
}
