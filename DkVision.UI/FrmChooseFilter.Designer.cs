namespace DkVision.UI
{
    partial class FrmChooseFilter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcSteps = new System.Windows.Forms.TabControl();
            this.tpageStep1 = new System.Windows.Forms.TabPage();
            this.tpageStep2 = new System.Windows.Forms.TabPage();
            this.propGridFilter = new System.Windows.Forms.PropertyGrid();
            this.lsbFilters = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnPrevStep = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnNextStep = new System.Windows.Forms.ToolStripStatusLabel();
            this.tcSteps.SuspendLayout();
            this.tpageStep1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcSteps
            // 
            this.tcSteps.Controls.Add(this.tpageStep1);
            this.tcSteps.Controls.Add(this.tpageStep2);
            this.tcSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSteps.Location = new System.Drawing.Point(0, 0);
            this.tcSteps.Name = "tcSteps";
            this.tcSteps.SelectedIndex = 0;
            this.tcSteps.Size = new System.Drawing.Size(800, 424);
            this.tcSteps.TabIndex = 0;
            // 
            // tpageStep1
            // 
            this.tpageStep1.Controls.Add(this.propGridFilter);
            this.tpageStep1.Controls.Add(this.lsbFilters);
            this.tpageStep1.Location = new System.Drawing.Point(4, 25);
            this.tpageStep1.Name = "tpageStep1";
            this.tpageStep1.Padding = new System.Windows.Forms.Padding(3);
            this.tpageStep1.Size = new System.Drawing.Size(792, 395);
            this.tpageStep1.TabIndex = 0;
            this.tpageStep1.Text = "Choose filter type";
            this.tpageStep1.UseVisualStyleBackColor = true;
            // 
            // tpageStep2
            // 
            this.tpageStep2.Location = new System.Drawing.Point(4, 25);
            this.tpageStep2.Name = "tpageStep2";
            this.tpageStep2.Padding = new System.Windows.Forms.Padding(3);
            this.tpageStep2.Size = new System.Drawing.Size(792, 395);
            this.tpageStep2.TabIndex = 1;
            this.tpageStep2.Text = "Choose shape";
            this.tpageStep2.UseVisualStyleBackColor = true;
            // 
            // propGridFilter
            // 
            this.propGridFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propGridFilter.Location = new System.Drawing.Point(225, 3);
            this.propGridFilter.Name = "propGridFilter";
            this.propGridFilter.Size = new System.Drawing.Size(564, 389);
            this.propGridFilter.TabIndex = 0;
            // 
            // lsbFilters
            // 
            this.lsbFilters.Dock = System.Windows.Forms.DockStyle.Left;
            this.lsbFilters.FormattingEnabled = true;
            this.lsbFilters.ItemHeight = 16;
            this.lsbFilters.Location = new System.Drawing.Point(3, 3);
            this.lsbFilters.Name = "lsbFilters";
            this.lsbFilters.Size = new System.Drawing.Size(222, 389);
            this.lsbFilters.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrevStep,
            this.toolStripStatusLabel2,
            this.btnNextStep});
            this.statusStrip1.Location = new System.Drawing.Point(0, 424);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnPrevStep
            // 
            this.btnPrevStep.Name = "btnPrevStep";
            this.btnPrevStep.Size = new System.Drawing.Size(37, 20);
            this.btnPrevStep.Text = "Prev";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(708, 20);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // btnNextStep
            // 
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(40, 20);
            this.btnNextStep.Text = "Next";
            // 
            // FrmChooseFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tcSteps);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FrmChooseFilter";
            this.Text = "Select filter";
            this.tcSteps.ResumeLayout(false);
            this.tpageStep1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcSteps;
        private System.Windows.Forms.TabPage tpageStep1;
        private System.Windows.Forms.TabPage tpageStep2;
        private System.Windows.Forms.PropertyGrid propGridFilter;
        private System.Windows.Forms.ListBox lsbFilters;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel btnPrevStep;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel btnNextStep;
    }
}