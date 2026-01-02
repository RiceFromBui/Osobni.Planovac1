using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Osobni.Planovac1
{
    public class SearchForm : Form
    {
        private DataGridView grid;
        private ComboBox cmbFilter;
        private Button btnRefresh;

        public SearchForm()
        {
            this.Text = "Vyhledávání a Přehled aktivit";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 1. Lišta filtru
            var panelTop = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.WhiteSmoke };

            var lblFilter = new Label { Text = "Filtr kategorie:", Location = new Point(20, 15), AutoSize = true };
            cmbFilter = new ComboBox { Location = new Point(120, 12), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbFilter.Items.Add("Všechny"); // Default
            cmbFilter.SelectedIndex = 0;
            cmbFilter.SelectedIndexChanged += (s, e) => LoadData(); // Automatický refresh

            panelTop.Controls.Add(lblFilter);
            panelTop.Controls.Add(cmbFilter);

            // 2. Tabulka výsledků
            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };
            grid.Columns.Add("Date", "Datum");
            grid.Columns.Add("Time", "Čas");
            grid.Columns.Add("Category", "Kategorie");
            grid.Columns.Add("Text", "Aktivita");

            // Přidání do okna
            this.Controls.Add(grid);
            this.Controls.Add(panelTop);

            LoadData(); // Načíst data při startu
        }

        private void LoadData()
        {
            grid.Rows.Clear();
            string selectedCat = cmbFilter.SelectedItem?.ToString() ?? "Všechny";

            // Načtení všech dat
            var allData = EventStorage.LoadAll();

            // Unikátní seznam kategorií pro naplnění filtru (pokud bychom chtěli dynamicky)
            HashSet<string> categoriesFound = new HashSet<string> { "Všechny" };

            // Procházení dat
            foreach (var datePair in allData) // Datum
            {
                foreach (var timePair in datePair.Value) // Čas
                {
                    EventModel ev = timePair.Value;
                    categoriesFound.Add(ev.Category);

                    // Filtrace
                    if (selectedCat == "Všechny" || ev.Category == selectedCat)
                    {
                        grid.Rows.Add(datePair.Key, timePair.Key, ev.Category, ev.Text);
                    }
                }
            }

            // Aktualizace seznamu v ComboBoxu jen poprvé (aby se nám nemizely kategorie při filtrování)
            if (cmbFilter.Items.Count == 1)
            {
                foreach (var cat in categoriesFound)
                {
                    if (cat != "Všechny") cmbFilter.Items.Add(cat);
                }
            }
        }
    }
}