// File: UserControl1Days.cs
using System;
using System.Windows.Forms;

namespace Osobni.Planovac1
{
    public partial class DayControl : UserControl
    {
        public int DayNumber { get; private set; }
        public event EventHandler<int> DayClicked;

        public DayControl()
        {
            InitializeComponent();
            this.Click += OnControlClick;
            foreach (Control control in this.Controls)
            {
                control.Click += OnControlClick;
            }
        }

        private void OnControlClick(object sender, EventArgs e)
        {
            DayClicked?.Invoke(this, DayNumber);
        }

        public void days(int numday, int month, int year)
        {
            DayNumber = numday;
            lbdays.Text = numday.ToString();

            DateTime date = new DateTime(year, month, numday);
            var notes = EventStorage.LoadAll();
            string key = date.ToString("yyyy-MM-dd");

            if (notes.ContainsKey(key) && notes[key].Count > 0)
            {
                this.BackColor = Color.LightYellow;
            }


        }
    }
}
