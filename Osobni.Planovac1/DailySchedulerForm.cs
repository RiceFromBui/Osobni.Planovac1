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
        private Dictionary<string, EventModel> dailyEntries = new();
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
            // Tady musíme správně načíst ten slovník modelů
            var allData = EventStorage.LoadAll();
            string key = date.ToString("yyyy-MM-dd");

            if (allData.ContainsKey(key))
                dailyEntries = allData[key];
            else
                dailyEntries = new Dictionary<string, EventModel>();

            RefreshSlots();
        }

        private void RefreshSlots()
        {
            tblTimeline.SuspendLayout();
            tblTimeline.Controls.Clear();
            tblTimeline.RowStyles.Clear();

            // 1. Vytvoříme seznam všech časů, které chceme zobrazit
            List<string> timeSlots = new List<string>();

            // Přidáme standardní hodiny 00:00 - 23:00
            for (int i = 0; i < 24; i++)
            {
                timeSlots.Add(i.ToString("D2") + ":00");
            }

            // Přidáme časy z uložených událostí (pokud tam už nejsou), např. "14:30"
            foreach (var key in dailyEntries.Keys)
            {
                if (!timeSlots.Contains(key))
                {
                    timeSlots.Add(key);
                }
            }

            // Seřadíme časy chronologicky, aby 14:30 bylo pod 14:00
            timeSlots.Sort();

            // 2. Nastavíme tabulku
            tblTimeline.RowCount = timeSlots.Count; // Počet řádků podle počtu časů

            // 3. Vykreslíme řádky
            for (int i = 0; i < timeSlots.Count; i++)
            {
                string time = timeSlots[i];

                // Nastavíme výšku řádku
                tblTimeline.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

                // Label s časem
                var lblTime = new Label
                {
                    Text = time,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Padding = new Padding(2)
                };

                // Panel pro událost
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
                    // ... (Zbytek kódu pro vykreslení existující události zůstává stejný) ...
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
                        Text = $"[{dailyEntries[time].Category}] {dailyEntries[time].Text}",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Padding = new Padding(6, 4, 6, 4),
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        AutoEllipsis = true,
                        BackColor = Color.White
                    };

                    var btnEdit = new Button { Text = "✎", Dock = DockStyle.Fill, Margin = new Padding(0) };
                    btnEdit.Click += (s, e) => OnTimeSlotClick(time);

                    var btnDelete = new Button { Text = "✖", Dock = DockStyle.Fill, Margin = new Padding(0) };
                    btnDelete.Click += (s, e) => OnDeleteTimeSlot(time);

                    innerLayout.Controls.Add(label, 0, 0);
                    innerLayout.Controls.Add(btnEdit, 1, 0);
                    innerLayout.Controls.Add(btnDelete, 2, 0);
                    panelSlot.Controls.Add(innerLayout);
                }
                else
                {
                    // Kliknutí na prázdný panel otevře přidání události
                    panelSlot.Click += (s, ev) => OnTimeSlotClick(time);
                }

                tblTimeline.Controls.Add(lblTime, 0, i);
                tblTimeline.Controls.Add(panelSlot, 1, i);
            }
            tblTimeline.ResumeLayout();
        }

        private void OnTimeSlotClick(string time)
        {
            EventModel currentData = dailyEntries.ContainsKey(time) ? dailyEntries[time] : null;

            using (var form = new TimeEventForm(time, currentData))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Smazání starého času při změně
                    if (time != form.ResultTime && dailyEntries.ContainsKey(time))
                    {
                        // Jen pokud tam něco bylo
                        if (currentData != null) dailyEntries.Remove(time);
                    }

                    // Uložení
                    if (!string.IsNullOrWhiteSpace(form.ResultEvent.Text))
                    {
                        dailyEntries[form.ResultTime] = form.ResultEvent;
                    }
                    else
                    {
                        dailyEntries.Remove(form.ResultTime);
                    }

                    RefreshSlots();
                }
            }
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
            // Musíme uložit každou položku zvlášť nebo upravit storage na ukládání celého dne.
            // Pro jednoduchost projdeme položky:
            DateTime date = new DateTime(year, month, day);

            // Nejprve načteme vše, vymažeme dnešek a nahrajeme nové (aby zmizely smazané)
            var all = EventStorage.LoadAll();
            string key = date.ToString("yyyy-MM-dd");

            if (dailyEntries.Count > 0)
                all[key] = dailyEntries;
            else
                all.Remove(key);

            EventStorage.SaveAll(all);

            MessageBox.Show("Uloženo.", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void btnAddCustomTime_Click(object sender, EventArgs e)
        {
            // 1. Zeptáme se na čas
            string timeInput = Microsoft.VisualBasic.Interaction.InputBox(
                "Zadej čas události (např. 14:30):",
                "Přidat vlastní čas",
                "12:00");

            if (string.IsNullOrWhiteSpace(timeInput)) return;

            if (!timeInput.Contains(":"))
            {
                MessageBox.Show("Čas musí být ve formátu HH:MM", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Zeptáme se na text události
            string noteInput = Microsoft.VisualBasic.Interaction.InputBox(
                $"Zadej událost pro {timeInput}:",
                "Nová událost",
                "");

            if (!string.IsNullOrWhiteSpace(noteInput))
            {
                // --- ZDE BYLA CHYBA (FIX) ---
                // Nemůžeme uložit jen text (string). Musíme vytvořit EventModel.
                // Jako výchozí kategorii dáme třeba "Vlastní" nebo "Obecné".
                dailyEntries[timeInput] = new EventModel
                {
                    Text = noteInput,
                    Category = "Vlastní"
                };
                // ----------------------------

                RefreshSlots();
            }
        }

        private void scrollPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
