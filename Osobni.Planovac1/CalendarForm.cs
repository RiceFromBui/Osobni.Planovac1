using System;
using System.Globalization;
using System.Windows.Forms;

namespace Osobni.Planovac1
{
    public partial class CalendarForm : Form //hlavní okno aplikace
    {
        private int month, year;
        private NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer notificationTimer;

        public CalendarForm()
        {
            InitializeComponent(); 
            //Nastavení notifikací
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information; 
            notifyIcon.Visible = true;
            notifyIcon.Text = "Osobní plánovač běží";
            notificationTimer = new System.Windows.Forms.Timer();
            notificationTimer.Interval = 60000; 
            notificationTimer.Tick += CheckForUpcomingEvents; 
            notificationTimer.Start();

            // Tlačítko pro vyhledávání
            Button btnSearch = new Button();
            btnSearch.Text = "🔍 Přehled aktivit";
            btnSearch.Size = new Size(150, 30);
            btnSearch.Location = new Point(12, 12); // Nahoře vlevo
            btnSearch.Click += (s, e) => {
                SearchForm search = new SearchForm();
                search.ShowDialog();
            };
            this.Controls.Add(btnSearch);
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

        private void DisplayMonth(int year, int month) //Zobrazí dny v kalendáři
        {
            daycontainer.Controls.Clear();

            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lblMonthName.Text = $"{monthName} {year}";

            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            for (int i = 0; i < startDayOfWeek; i++)
            {
                daycontainer.Controls.Add(new UserControlBlank());
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                var dayControl = new DayControl();
                dayControl.days(day, month, year);
                dayControl.DayClicked += (s, selectedDay) => OpenDayDetail(selectedDay);
                daycontainer.Controls.Add(dayControl);
            }
        }

        private void OpenDayDetail(int day) //Otevře detail dne
        {
            var schedulerForm = new DailySchedulerForm(day, month, year, () => DisplayMonth(year, month));
            schedulerForm.ShowDialog();
        }



        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void daycontainer_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CheckForUpcomingEvents(object sender, EventArgs e)
        {
            DateTime targetTime = DateTime.Now.AddHours(1);
            string timeKey = targetTime.ToString("HH:mm");
            string dateKey = DateTime.Now.ToString("yyyy-MM-dd");

            var allEvents = EventStorage.LoadAll();

            if (allEvents.ContainsKey(dateKey))
            {
                var todaysEvents = allEvents[dateKey];

                if (todaysEvents.ContainsKey(timeKey))
                {
                    EventModel udalost = todaysEvents[timeKey];
                    string eventNote = udalost.Text;           
                    notifyIcon.ShowBalloonTip(5000,"Blíží se událost! ⏳",$"Za hodinu ({timeKey}): {eventNote} ({udalost.Category})",ToolTipIcon.Info);
                }
            }
        }
    }
}
