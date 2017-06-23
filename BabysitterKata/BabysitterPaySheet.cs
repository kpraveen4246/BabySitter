using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabysitterKata
{
    public class BabysitterPaySheet
    {
        /// <summary>
        /// Pay owed to a babysitter for one night's work.
        /// </summary>
        public int Pay { get { return _pay; } }

        private const int BEFORE_BEDTIME_PAY_RATE = 12;
        private const int BEDTIME_TO_MIDNIGHT_PAY_RATE = 8;
        private const int AFTER_MIDNIGHT_PAY_RATE = 16;

        private int _pay;

        /// <summary>
        /// Constructor if babysitter stops work before midnight.
        /// </summary>
        /// <param name="hrsBeforeBd">Hours worked before bedtime as calculated by <see cref="BabysitterPaySheet"/></param>
        /// <param name="hrsBedToMidnight">Hours worked after bedtime but before midnight as calculated by <see cref="BabysitterPaySheet"/></param>
        public BabysitterPaySheet(int hrsBeforeBd, int hrsBedToMidnight)
        {
            _pay = (hrsBeforeBd * BEFORE_BEDTIME_PAY_RATE) + (hrsBedToMidnight * BEDTIME_TO_MIDNIGHT_PAY_RATE);
        }

        /// <summary>
        /// Constructor if babysitter works past midnight.
        /// </summary>
        /// <param name="hrsBeforeBed">Hours worked before bedtime as calculated by <see cref="BabysitterPaySheet"/></param>
        /// <param name="hrsBedToMidnight">Hours worked after bedtime but before midnight as calculated by <see cref="BabysitterPaySheet"/></param>
        /// <param name="hrsAfterMidnight">Hours worked after midnight as calculated by <see cref="BabysitterPaySheet"/></param>
        public BabysitterPaySheet(int hrsBeforeBed, int hrsBedToMidnight, int hrsAfterMidnight)
        {
            _pay = (hrsBeforeBed * BEFORE_BEDTIME_PAY_RATE) + (hrsBedToMidnight * BEDTIME_TO_MIDNIGHT_PAY_RATE) + (hrsAfterMidnight * AFTER_MIDNIGHT_PAY_RATE);
        }
    }
}
