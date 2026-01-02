namespace Osobni.Planovac1
{
    partial class DailySchedulerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            scrollPanel = new Panel();
            tblTimeline = new TableLayoutPanel();
            btnSaveAll = new Button();
            btnExport = new Button();
            btnClearAll = new Button();
            scrollPanel.SuspendLayout();
            SuspendLayout();
            // 
            // scrollPanel
            // 
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(tblTimeline);
            scrollPanel.Dock = DockStyle.Top;
            scrollPanel.Location = new Point(0, 0);
            scrollPanel.Name = "scrollPanel";
            scrollPanel.Size = new Size(424, 675);
            scrollPanel.TabIndex = 4;
            scrollPanel.Paint += scrollPanel_Paint;
            // 
            // tblTimeline
            // 
            tblTimeline.AutoSize = true;
            tblTimeline.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tblTimeline.ColumnCount = 2;
            tblTimeline.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 52F));
            tblTimeline.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblTimeline.Dock = DockStyle.Top;
            tblTimeline.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tblTimeline.Location = new Point(0, 0);
            tblTimeline.Name = "tblTimeline";
            tblTimeline.RowCount = 1;
            tblTimeline.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblTimeline.Size = new Size(424, 20);
            tblTimeline.TabIndex = 0;
            // 
            // btnSaveAll
            // 
            btnSaveAll.Location = new Point(10, 694);
            btnSaveAll.Name = "btnSaveAll";
            btnSaveAll.Size = new Size(402, 28);
            btnSaveAll.TabIndex = 1;
            btnSaveAll.Text = "Uložit vše";
            btnSaveAll.UseVisualStyleBackColor = true;
            btnSaveAll.Click += btnSaveAll_Click;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(10, 731);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(402, 28);
            btnExport.TabIndex = 2;
            btnExport.Text = "Exportovat den";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.Location = new Point(10, 769);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(402, 28);
            btnClearAll.TabIndex = 3;
            btnClearAll.Text = "Vymazat celý den";
            btnClearAll.UseVisualStyleBackColor = true;
            btnClearAll.Click += btnClearAll_Click;
            // 
            // DailySchedulerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(424, 807);
            Controls.Add(btnClearAll);
            Controls.Add(btnExport);
            Controls.Add(btnSaveAll);
            Controls.Add(scrollPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DailySchedulerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Denní plán";
            Load += DailySchedulerForm_Load;
            scrollPanel.ResumeLayout(false);
            scrollPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel scrollPanel;
        private System.Windows.Forms.TableLayoutPanel tblTimeline;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClearAll;
    }
}