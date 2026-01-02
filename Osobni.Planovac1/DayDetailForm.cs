// File: DayDetailForm.cs
using System;
using System.Windows.Forms;

namespace Osobni.Planovac1
{
    public partial class DayDetailForm : Form
    {
        private int day, month, year;
        private readonly Action reloadCalendar;

        public DayDetailForm(int day, int month, int year, Action reloadCalendar)
        {
            InitializeComponent();
            this.day = day;
            this.month = month;
            this.year = year;
            this.reloadCalendar = reloadCalendar;
            this.Text = $"Detail pro {day}.{month}.{year}";
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            string note = txtNote.Text;
            DateTime selectedDate = new DateTime(year, month, day);
            NoteStorage.SaveNote(selectedDate, note);
            reloadCalendar?.Invoke(); // ⚡️ aktualizuj UI
            this.Close();
        }


    }
}
