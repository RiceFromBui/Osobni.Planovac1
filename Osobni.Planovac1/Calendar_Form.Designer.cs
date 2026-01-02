namespace Osobni.Planovac1
{
    partial class Calendar_Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            daycontainer = new FlowLayoutPanel();
            btnnext = new Button();
            btnprev = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            lblMonthName = new Label();
            SuspendLayout();
            // 
            // daycontainer
            // 
            daycontainer.Location = new Point(12, 119);
            daycontainer.Name = "daycontainer";
            daycontainer.Size = new Size(1104, 711);
            daycontainer.TabIndex = 0;
            daycontainer.Paint += daycontainer_Paint;
            // 
            // btnnext
            // 
            btnnext.Location = new Point(1041, 836);
            btnnext.Name = "btnnext";
            btnnext.Size = new Size(75, 23);
            btnnext.TabIndex = 1;
            btnnext.Text = "Další";
            btnnext.UseVisualStyleBackColor = true;
            btnnext.Click += btnnext_Click_1;
            // 
            // btnprev
            // 
            btnprev.Location = new Point(960, 836);
            btnprev.Name = "btnprev";
            btnprev.Size = new Size(75, 23);
            btnprev.TabIndex = 2;
            btnprev.Text = "Zpět";
            btnprev.UseVisualStyleBackColor = true;
            btnprev.Click += btnprev_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Poppins", 12F);
            label1.Location = new Point(55, 83);
            label1.Name = "label1";
            label1.Size = new Size(68, 28);
            label1.TabIndex = 3;
            label1.Text = "Neděle";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Poppins", 12F);
            label2.Location = new Point(216, 83);
            label2.Name = "label2";
            label2.Size = new Size(70, 28);
            label2.TabIndex = 4;
            label2.Text = "Pondělí";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Poppins", 12F);
            label3.Location = new Point(535, 83);
            label3.Name = "label3";
            label3.Size = new Size(65, 28);
            label3.TabIndex = 6;
            label3.Text = "Středa";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Poppins", 12F);
            label4.Location = new Point(377, 83);
            label4.Name = "label4";
            label4.Size = new Size(54, 28);
            label4.TabIndex = 5;
            label4.Text = "Úterý";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Poppins", 12F);
            label5.Location = new Point(846, 83);
            label5.Name = "label5";
            label5.Size = new Size(56, 28);
            label5.TabIndex = 8;
            label5.Text = "Pátek";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Poppins", 12F);
            label6.Location = new Point(687, 83);
            label6.Name = "label6";
            label6.Size = new Size(69, 28);
            label6.TabIndex = 7;
            label6.Text = "Čtvrtek";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Poppins", 12F);
            label7.Location = new Point(995, 83);
            label7.Name = "label7";
            label7.Size = new Size(69, 28);
            label7.TabIndex = 9;
            label7.Text = "Sobota";
            // 
            // lblMonthName
            // 
            lblMonthName.AutoSize = true;
            lblMonthName.Font = new Font("Poppins", 18F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblMonthName.Location = new Point(490, 23);
            lblMonthName.Name = "lblMonthName";
            lblMonthName.Size = new Size(144, 42);
            lblMonthName.TabIndex = 10;
            lblMonthName.Text = "MESIC ROK";
            lblMonthName.TextAlign = ContentAlignment.TopCenter;
            // 
            // Calendar_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1144, 871);
            Controls.Add(lblMonthName);
            Controls.Add(label7);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnprev);
            Controls.Add(btnnext);
            Controls.Add(daycontainer);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Calendar_Form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel daycontainer;
        private Button btnnext;
        private Button btnprev;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblMonthName;
    }
}
