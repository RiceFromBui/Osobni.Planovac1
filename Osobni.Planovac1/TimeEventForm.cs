using System;
using System.Drawing;
using System.Windows.Forms;

namespace Osobni.Planovac1
{
    public class TimeEventForm : Form
    {
        public string ResultTime { get; private set; }
        public EventModel ResultEvent { get; private set; } // Vracíme celý objekt

        private TextBox txtTime;
        private TextBox txtNote;
        private ComboBox cmbCategory;
        private Button btnOk;
        private Button btnCancel;

        public TimeEventForm(string baseTime, EventModel existingData = null)
        {
            this.Text = (existingData == null) ? "Nová událost" : "Upravit událost";
            this.Size = new Size(350, 280); // Zvětšili jsme okno
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // 1. Čas
            var lblTime = new Label { Text = "Čas:", Location = new Point(15, 20), AutoSize = true };
            txtTime = new TextBox { Location = new Point(15, 45), Width = 100, Text = baseTime };

            // 2. Kategorie (NOVÉ)
            var lblCat = new Label { Text = "Kategorie:", Location = new Point(150, 20), AutoSize = true };
            cmbCategory = new ComboBox { Location = new Point(150, 45), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            // Naplníme kategorie
            cmbCategory.Items.AddRange(new object[] { "Práce", "Škola", "Zábava", "Sport", "Ostatní", "--- Vlastní ---" });

            // Nastavení vybrané kategorie
            if (existingData != null)
            {
                if (!cmbCategory.Items.Contains(existingData.Category))
                    cmbCategory.Items.Insert(0, existingData.Category); // Přidáme ji, pokud je vlastní
                cmbCategory.SelectedItem = existingData.Category;
            }
            else
            {
                cmbCategory.SelectedIndex = 4; // Default "Ostatní"
            }

            // Logika pro "Vlastní"
            cmbCategory.SelectedIndexChanged += (s, e) => {
                if (cmbCategory.SelectedItem.ToString() == "--- Vlastní ---")
                {
                    string newCat = Microsoft.VisualBasic.Interaction.InputBox("Zadej název nové kategorie:", "Vlastní kategorie", "");
                    if (!string.IsNullOrWhiteSpace(newCat))
                    {
                        cmbCategory.Items.Insert(0, newCat);
                        cmbCategory.SelectedItem = newCat;
                    }
                    else
                    {
                        cmbCategory.SelectedIndex = 0; // Zpět na první
                    }
                }
            };

            // 3. Text
            var lblNote = new Label { Text = "Popis události:", Location = new Point(15, 80), AutoSize = true };
            txtNote = new TextBox { Location = new Point(15, 105), Width = 290, Height = 60, Multiline = true }; // Větší pole
            if (existingData != null) txtNote.Text = existingData.Text;

            // Tlačítka
            btnOk = new Button { Text = "Uložit", Location = new Point(80, 190), DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Zrušit", Location = new Point(170, 190), DialogResult = DialogResult.Cancel };

            this.Controls.AddRange(new Control[] { lblTime, txtTime, lblCat, cmbCategory, lblNote, txtNote, btnOk, btnCancel });
            this.AcceptButton = btnOk;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (this.DialogResult == DialogResult.OK)
            {
                if (!TimeSpan.TryParse(txtTime.Text, out _))
                {
                    MessageBox.Show("Neplatný čas!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                ResultTime = txtTime.Text;
                ResultEvent = new EventModel
                {
                    Text = txtNote.Text,
                    Category = cmbCategory.SelectedItem.ToString()
                };
            }
        }
    }
}