// File: DailySchedulerForm.Designer.cs

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
            this.scrollPanel = new System.Windows.Forms.Panel();
            this.tblTimeline = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.scrollPanel.SuspendLayout();
            this.SuspendLayout();

            // scrollPanel
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.scrollPanel.Location = new System.Drawing.Point(12, 12);
            this.scrollPanel.Name = "scrollPanel";
            this.scrollPanel.Size = new System.Drawing.Size(460, 720);
            this.scrollPanel.Controls.Add(this.tblTimeline);

            // tblTimeline
            this.tblTimeline.AutoSize = true;
            this.tblTimeline.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblTimeline.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblTimeline.ColumnCount = 2;
            this.tblTimeline.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblTimeline.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTimeline.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblTimeline.Location = new System.Drawing.Point(0, 0);
            this.tblTimeline.Name = "tblTimeline";
            this.tblTimeline.RowCount = 1;
            this.tblTimeline.Size = new System.Drawing.Size(460, 0);
            this.tblTimeline.TabIndex = 0;

            // btnSaveAll
            this.btnSaveAll.Location = new System.Drawing.Point(12, 740);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(460, 30);
            this.btnSaveAll.TabIndex = 1;
            this.btnSaveAll.Text = "Uložit vše";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);

            // btnExport
            this.btnExport.Location = new System.Drawing.Point(12, 780);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(460, 30);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Exportovat den";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            // btnClearAll
            this.btnClearAll.Location = new System.Drawing.Point(12, 820);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(460, 30);
            this.btnClearAll.TabIndex = 3;
            this.btnClearAll.Text = "Vymazat celý den";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);

            // DailySchedulerForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 861);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSaveAll);
            this.Controls.Add(this.scrollPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DailySchedulerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Denní plán";
            this.Load += new System.EventHandler(this.DailySchedulerForm_Load);
            this.scrollPanel.ResumeLayout(false);
            this.scrollPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel scrollPanel;
        private System.Windows.Forms.TableLayoutPanel tblTimeline;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClearAll;
    }
}