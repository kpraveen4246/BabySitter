using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BabysitterKata
{
    public partial class BabysitterPayCalculator_UI : Form
    {
        public BabysitterPayCalculator_UI()
        {
            InitializeComponent();
        }

        private void TimeCardSubmit_Button_Click(object sender, EventArgs e)
        {
            TwentyFourHourTime startTime;
            TwentyFourHourTime endTime;
            TwentyFourHourTime bedTime;
            BabysitterTimeCard timeCard;
            BabysitterPaySheet paySheet;

            startTime = InitializeTime((TIME_OF_DAY)StartTime_AMPM_ComboBox.SelectedItem, StartTimeHour_NumericUpDown.Value, StartTimeMinute_NumericUpDown.Value);
            endTime = InitializeTime((TIME_OF_DAY)EndTime_AMPM_ComboBox.SelectedItem, EndTimeHour_NumericUpDown.Value, EndTimeMinute_NumericUpDown.Value);

            bedTime = InitializeTime((TIME_OF_DAY)BedTime_AMPM_ComboBox.SelectedItem, BedTimeHour_NumericUpDown.Value, BedTimeMinute_NumericUpDown.Value);
            try
            {
                timeCard = new BabysitterTimeCard(startTime, endTime, bedTime);

                if (timeCard.CalculateHoursAfterMidnight() != 0)
                {
                    paySheet = new BabysitterPaySheet(timeCard.CalculateHoursBeforeBedtime(), timeCard.CalculateHoursBetweenBedtimeAndMidnight(), timeCard.CalculateHoursAfterMidnight());
                }
                else
                {
                    paySheet = new BabysitterPaySheet(timeCard.CalculateHoursBeforeBedtime(), timeCard.CalculateHoursBetweenBedtimeAndMidnight());
                }

                UpdatePaysheet(timeCard, paySheet);
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Time Sheet Error");
            }
        }

        private void UpdatePaysheet(BabysitterTimeCard timeCard, BabysitterPaySheet paySheet)
        {
            HoursBeforeBedtime_TextBox.Text = timeCard.CalculateHoursBeforeBedtime().ToString();
            HoursBedTimeToMidnight_TextBox.Text = timeCard.CalculateHoursBetweenBedtimeAndMidnight().ToString();
            HoursAfterMidnight_TextBox.Text = timeCard.CalculateHoursAfterMidnight().ToString();

            Pay_TextBox.Text = paySheet.Pay.ToString();
        }

        private TwentyFourHourTime InitializeTime(TIME_OF_DAY tod, decimal hour, decimal minute)
        {
            switch (tod)
            {
                case TIME_OF_DAY.AM:
                    return InitializeAMTime(hour, minute);
                case TIME_OF_DAY.PM:
                    return InitializePMTime(hour, minute);
                default:
                    throw new InvalidEnumArgumentException("No Time of Day Selected");
            }
        }

        private TwentyFourHourTime InitializeAMTime(decimal hour, decimal minute)
        {
            if (hour == 12)
                hour = 0;
            return new TwentyFourHourTime((int)hour, (int)minute);
        }

        private TwentyFourHourTime InitializePMTime(decimal hour, decimal minute)
        {
            int realHour = (int)hour + 12;

            if (realHour == 24)
                realHour = 12;

            return new TwentyFourHourTime(realHour, (int)minute);
        }
    }
}
