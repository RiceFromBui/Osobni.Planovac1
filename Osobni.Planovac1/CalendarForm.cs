// File: Form1.cs
using System;
using System.Globalization;
using System.Windows.Forms;

namespace Osobni.Planovac1
{
    public partial class CalendarForm : Form
    {
        private int month, year;
        private NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer notificationTimer;

        public CalendarForm()
        {
            InitializeComponent();

            // --- NASTAVENÍ NOTIFIKACÍ ---

            // 1. Vytvoříme ikonku pro oznamovací oblast (systémová lišta)
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information; // Můžeš použít vlastní ikonu .ico
            notifyIcon.Visible = true;
            notifyIcon.Text = "Osobní plánovač běží";

            // 2. Vytvoříme časovač, který tiká každou minutu
            notificationTimer = new System.Windows.Forms.Timer();
            notificationTimer.Interval = 60000; // 60 000 ms = 1 minuta
            notificationTimer.Tick += CheckForUpcomingEvents; // Metoda, co se spustí
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

        private void DisplayMonth(int year, int month)
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

        private void OpenDayDetail(int day)
        {
            var schedulerForm = new DailySchedulerForm(day, month, year, () => DisplayMonth(year, month));
            schedulerForm.ShowDialog();
        }



        private void label2_Click(object sender, EventArgs e)
        {
            // Optional: handle label click if needed
        }

        private void daycontainer_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CheckForUpcomingEvents(object sender, EventArgs e)
        {
            // 1. Zjistíme, kolik je hodin a přičteme 1 hodinu (protože chceme varovat předem)
            DateTime targetTime = DateTime.Now.AddHours(1);

            // Získáme formát času "14:30" (stejný, jako ukládáme do souboru)
            string timeKey = targetTime.ToString("HH:mm");

            // Získáme dnešní datum pro klíč do slovníku
            string dateKey = DateTime.Now.ToString("yyyy-MM-dd");

            // 2. Načteme data
            var allEvents = EventStorage.LoadAll();

            // 3. Podíváme se, jestli pro DNEŠEK existuje nějaký záznam
            if (allEvents.ContainsKey(dateKey))
            {
                var todaysEvents = allEvents[dateKey];

                // 4. Podíváme se, jestli v dnešním dni existuje událost v tento čas
                if (todaysEvents.ContainsKey(timeKey))
                {
                    string eventNote = todaysEvents[timeKey];

                    // 5. Zobrazíme bublinu (notifikaci)
                    notifyIcon.ShowBalloonTip(
                        5000,                          // Jak dlouho má svítit (ms)
                        "Blíží se událost! ⏳",         // Nadpis
                        $"Za hodinu ({timeKey}): {eventNote}", // Text zprávy
                        ToolTipIcon.Info               // Ikona
                    );
                }
            }
        }
    }
}
