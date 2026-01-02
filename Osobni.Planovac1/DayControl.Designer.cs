namespace Osobni.Planovac1
{
    partial class DayControl
    {
        /// <summary> 
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód vygenerovaný pomocí Návrháře komponent

        /// <summary> 
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            lbdays = new Label();
            SuspendLayout();
            // 
            // lbdays
            // 
            lbdays.AutoSize = true;
            lbdays.Font = new Font("Poppins", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lbdays.Location = new Point(14, 16);
            lbdays.Name = "lbdays";
            lbdays.Size = new Size(32, 28);
            lbdays.TabIndex = 0;
            lbdays.Text = "00";
            // 
            // UserControl1Days
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lbdays);
            Name = "UserControl1Days";
            Size = new Size(150, 108);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbdays;
    }
}
