// File: Form1.cs
using System;
using System.Globalization;
using System.Windows.Forms;

namespace Osobni.Planovac1
{
    public partial class Form1 : Form
    {
        private int month, year;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            DisplayMonth(year, month);
        }

        private void btnnext_Click_1(object sender, EventArgs e)
        {
            if (month == 12)
            {
                month = 1;
                year++;
            }
            else
            {
                month++;
            }
            DisplayMonth(year, month);
        }

        private void btnprev_Click(object sender, EventArgs e)
        {
            if (month == 1)
            {
                month = 12;
                year--;
            }
            else
            {
                month--;
            }
            DisplayMonth(year, month);
        }

        private void DisplayMonth(int year, int month)
        {
            daycontainer.Controls.Clear();

            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATE.Text = $"{monthName} {year}";

            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            for (int i = 0; i < startDayOfWeek; i++)
            {
                daycontainer.Controls.Add(new UserControlBlank());
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                var dayControl = new UserControl1Days();
                dayControl.days(day, month, year);
                dayControl.DayClicked += (s, selectedDay) => OpenDayDetail(selectedDay);
                daycontainer.Controls.Add(dayControl);
            }
        }

        private void OpenDayDetail(int day)
        {
            var schedulerForm = new DailySchedulerForm(day, month, year, () => DisplayMonth(year, month));
            schedulerForm.ShowDialog();
        }



        private void label2_Click(object sender, EventArgs e)
        {
            // Optional: handle label click if needed
        }
    }
}
