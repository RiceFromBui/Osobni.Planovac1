using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Osobni.Planovac1
{
    public class TimeEventForm : Form
    {
        public string ResultTime { get; private set; }
        public EventModel ResultEvent { get; private set; }

        private TextBox txtTime;
        private TextBox txtNote;
        private ComboBox cmbCategory;
        private Button btnOk;
        private Button btnCancel;

        // Pomocné proměnné pro "uzamčení" předpony
        private bool isNewEvent;
        private string lockedPrefix = "";

        public TimeEventForm(string baseTime, EventModel existingData = null)
        {
            this.Text = (existingData == null) ? "Nová událost" : "Upravit událost";
            this.Size = new Size(350, 280);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Zjistíme, jestli jde o novou událost
            isNewEvent = (existingData == null);

            // 1. Čas
            var lblTime = new Label { Text = "Čas:", Location = new Point(15, 20), AutoSize = true };
            txtTime = new TextBox { Location = new Point(15, 45), Width = 100 };

            // --- LOGIKA PRO PŘEDVYPLNĚNÍ A UZAMČENÍ ---
            if (isNewEvent)
            {
                // Vezmeme hodinu a dvojtečku (např. "12:")
                lockedPrefix = baseTime.Split(':')[0] + ":";
                txtTime.Text = lockedPrefix;
                txtTime.SelectionStart = txtTime.Text.Length; // Kurzor na konec

                // Přidáme hlídače, aby nešlo smazat předponu
                txtTime.KeyDown += TxtTime_KeyDown;
                txtTime.KeyPress += TxtTime_KeyPress;
            }
            else
            {
                // Při úpravě povolíme vše
                txtTime.Text = baseTime;
            }
            // ------------------------------------------

            // 2. Kategorie
            var lblCat = new Label { Text = "Kategorie:", Location = new Point(150, 20), AutoSize = true };
            cmbCategory = new ComboBox { Location = new Point(150, 45), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCategory.Items.AddRange(new object[] { "Práce", "Škola", "Zábava", "Sport", "Ostatní", "--- Vlastní ---" });

            if (existingData != null)
            {
                if (!cmbCategory.Items.Contains(existingData.Category))
                    cmbCategory.Items.Insert(0, existingData.Category);
                cmbCategory.SelectedItem = existingData.Category;
            }
            else
            {
                cmbCategory.SelectedIndex = 4; // Default "Ostatní"
            }

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
                        cmbCategory.SelectedIndex = 0;
                    }
                }
            };

            // 3. Text
            var lblNote = new Label { Text = "Popis události:", Location = new Point(15, 80), AutoSize = true };
            txtNote = new TextBox { Location = new Point(15, 105), Width = 290, Height = 60, Multiline = true };
            if (existingData != null) txtNote.Text = existingData.Text;

            // Tlačítka
            btnOk = new Button { Text = "Uložit", Location = new Point(80, 190), DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Zrušit", Location = new Point(170, 190), DialogResult = DialogResult.Cancel };

            this.Controls.AddRange(new Control[] { lblTime, txtTime, lblCat, cmbCategory, lblNote, txtNote, btnOk, btnCancel });
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
            this.ActiveControl = txtTime;
        }

        // --- OCHRANA PROTI SMAZÁNÍ PŘEDPONY ---
        private void TxtTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (isNewEvent)
            {
                if (txtTime.SelectionStart <= lockedPrefix.Length && e.KeyCode == Keys.Back)
                {
                    e.SuppressKeyPress = true;
                }
                else if (txtTime.SelectionStart < lockedPrefix.Length && e.KeyCode == Keys.Delete)
                {
                    e.SuppressKeyPress = true;
                }
                else if (txtTime.SelectionStart <= lockedPrefix.Length && e.KeyCode == Keys.Left)
                {
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void TxtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isNewEvent && txtTime.SelectionStart < lockedPrefix.Length && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                txtTime.SelectionStart = txtTime.Text.Length;
            }
        }
        // -------------------------------------

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (this.DialogResult == DialogResult.OK)
            {
                // 1. KONTROLA POPISU (NOVÉ)
                if (string.IsNullOrWhiteSpace(txtNote.Text))
                {
                    MessageBox.Show("Musíš napsat popis aktivity!", "Chybějící popis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // Nepovolí zavřít okno
                    return;
                }

                string input = txtTime.Text.Trim();

                // 2. KONTROLA FORMÁTU ČASU
                var match = Regex.Match(input, @"^(\d{1,2}):(\d{2})$");

                if (!match.Success)
                {
                    MessageBox.Show("Zadej čas ve správném formátu (např. 08:45).\nMinuty musí mít dvě číslice.", "Chyba formátu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                int hour = int.Parse(match.Groups[1].Value);
                int minute = int.Parse(match.Groups[2].Value);

                if (hour > 23 || minute > 59)
                {
                    MessageBox.Show("Zadaný čas je mimo platný rozsah (00:00 - 23:59).", "Neplatný čas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                // 3. ULOŽENÍ
                ResultTime = $"{hour:D2}:{minute:D2}";

                ResultEvent = new EventModel
                {
                    Text = txtNote.Text,
                    Category = cmbCategory.SelectedItem.ToString()
                };
            }
        }
    }
}