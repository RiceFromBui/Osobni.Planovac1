// File: DailySchedulerForm.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Osobni.Planovac1
{
    public partial class DailySchedulerForm : Form
    {
        private int day, month, year;
        private readonly Action reloadCalendar;
        private Dictionary<string, string> dailyEntries = new();

        public DailySchedulerForm(int day, int month, int year, Action reloadCalendar)
        {
            InitializeComponent();
            this.day = day;
            this.month = month;
            this.year = year;
            this.reloadCalendar = reloadCalendar;
            this.Text = $"Denní plán: {day}.{month}.{year}";
        }

        private void DailySchedulerForm_Load(object sender, EventArgs e)
        {
            DateTime date = new DateTime(year, month, day);
            dailyEntries = EventStorage.LoadAll().GetValueOrDefault(date.ToString("yyyy-MM-dd"), new());
            RefreshSlots();
        }

        private void RefreshSlots()
        {
            tblTimeline.SuspendLayout();
            tblTimeline.RowCount = 24;
            tblTimeline.RowStyles.Clear();
            tblTimeline.Controls.Clear();

            for (int i = 0; i < 24; i++)
            {
                tblTimeline.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

                string time = i.ToString("D2") + ":00";

                var lblTime = new Label
                {
                    Text = time,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Padding = new Padding(2)
                };

                var panelSlot = new Panel
                {
                    Name = "panel_" + time.Replace(":", ""),
                    BackColor = Color.WhiteSmoke,
                    BorderStyle = BorderStyle.FixedSingle,
                    Dock = DockStyle.Fill,
                    Tag = time,
                    Padding = new Padding(2)
                };

                if (dailyEntries.ContainsKey(time))
                {
                    panelSlot.BackColor = Color.LightGreen;

                    var innerLayout = new TableLayoutPanel
                    {
                        ColumnCount = 3,
                        Dock = DockStyle.Fill,
                        RowCount = 1
                    };
                    innerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                    innerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
                    innerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));

                    var label = new Label
                    {
                        Text = dailyEntries[time],
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Padding = new Padding(6, 4, 6, 4),
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        AutoEllipsis = true,
                        BackColor = Color.White
                    };

                    var btnEdit = new Button
                    {
                        Text = "✎",
                        Dock = DockStyle.Fill,
                        Margin = new Padding(0)
                    };
                    btnEdit.Click += (s, e) => OnTimeSlotClick(time);

                    var btnDelete = new Button
                    {
                        Text = "✖",
                        Dock = DockStyle.Fill,
                        Margin = new Padding(0)
                    };
                    btnDelete.Click += (s, e) => OnDeleteTimeSlot(time);

                    innerLayout.Controls.Add(label, 0, 0);
                    innerLayout.Controls.Add(btnEdit, 1, 0);
                    innerLayout.Controls.Add(btnDelete, 2, 0);

                    panelSlot.Controls.Add(innerLayout);
                }
                else
                {
                    panelSlot.Click += (s, ev) => OnTimeSlotClick(time);
                }

                tblTimeline.Controls.Add(lblTime, 0, i);
                tblTimeline.Controls.Add(panelSlot, 1, i);
            }
            tblTimeline.ResumeLayout();
        }

        private void OnTimeSlotClick(string time)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox($"Zadej událost pro {time}", "Nová událost", dailyEntries.ContainsKey(time) ? dailyEntries[time] : "");

            if (!string.IsNullOrWhiteSpace(input))
                dailyEntries[time] = input;
            else
                dailyEntries.Remove(time);

            RefreshSlots();
        }

        private void OnDeleteTimeSlot(string time)
        {
            if (dailyEntries.ContainsKey(time))
            {
                dailyEntries.Remove(time);
                RefreshSlots();
            }
        }
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Opravdu chcete vymazat všechny události pro tento den?", "Potvrzení", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                dailyEntries.Clear();
                RefreshSlots();
            }
        }


        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            DateTime date = new DateTime(year, month, day);
            EventStorage.SaveAllForDate(date, dailyEntries);
            MessageBox.Show("Události byly uloženy.", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            reloadCalendar?.Invoke();
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DateTime date = new DateTime(year, month, day);
            using SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text file (*.txt)|*.txt|CSV file (*.csv)|*.csv";
            dialog.FileName = $"plan_{date:yyyy-MM-dd}";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                EventStorage.ExportDay(date, dialog.FileName);
                MessageBox.Show("Export dokončen.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
