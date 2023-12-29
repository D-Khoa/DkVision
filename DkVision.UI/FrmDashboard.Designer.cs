namespace DkVision.UI
{
    partial class FrmDashboard
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
            this.ucFrame = new DkVision.UI.Components.UcFrame();
            this.SuspendLayout();
            // 
            // ucFrame
            // 
            this.ucFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFrame.Location = new System.Drawing.Point(0, 0);
            this.ucFrame.Name = "ucFrame";
            this.ucFrame.Size = new System.Drawing.Size(800, 450);
            this.ucFrame.TabIndex = 0;
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ucFrame);
            this.Name = "FrmDashboard";
            this.Text = "FrmDashboard";
            this.ResumeLayout(false);

        }

        #endregion

        private Components.UcFrame ucFrame;
    }
}