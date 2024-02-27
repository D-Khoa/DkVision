namespace DkVision
{
    partial class FrmMain
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
            this.trvCameras = new System.Windows.Forms.TreeView();
            this.tBar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtShapeText = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMainQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.sBar = new System.Windows.Forms.StatusStrip();
            this.tcFrame = new System.Windows.Forms.TabControl();
            this.tPageMainFrame = new System.Windows.Forms.TabPage();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.dgvShapeViews = new System.Windows.Forms.DataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tPageFrames = new System.Windows.Forms.TabPage();
            this.frameMain = new DkVision.UserControls.UcFrame();
            this.btnRecord = new System.Windows.Forms.ToolStripButton();
            this.btnAutoAdjustment = new System.Windows.Forms.ToolStripButton();
            this.btnDrawRectangle = new System.Windows.Forms.ToolStripButton();
            this.btnDrawCirlce = new System.Windows.Forms.ToolStripButton();
            this.btnDrawPolygon = new System.Windows.Forms.ToolStripButton();
            this.btnRotate = new System.Windows.Forms.ToolStripButton();
            this.btnShowText = new System.Windows.Forms.ToolStripButton();
            this.btnDrawConfirm = new System.Windows.Forms.ToolStripButton();
            this.btnDrawCancel = new System.Windows.Forms.ToolStripButton();
            this.tBar.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.tcFrame.SuspendLayout();
            this.tPageMainFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShapeViews)).BeginInit();
            this.SuspendLayout();
            // 
            // trvCameras
            // 
            this.trvCameras.Dock = System.Windows.Forms.DockStyle.Left;
            this.trvCameras.Location = new System.Drawing.Point(2, 33);
            this.trvCameras.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trvCameras.Name = "trvCameras";
            this.trvCameras.Size = new System.Drawing.Size(110, 385);
            this.trvCameras.TabIndex = 0;
            // 
            // tBar
            // 
            this.tBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRecord,
            this.btnAutoAdjustment,
            this.toolStripSeparator2,
            this.btnDrawRectangle,
            this.btnDrawCirlce,
            this.btnDrawPolygon,
            this.btnRotate,
            this.btnShowText,
            this.toolStripSeparator1,
            this.txtShapeText,
            this.toolStripSeparator3,
            this.btnDrawConfirm,
            this.btnDrawCancel});
            this.tBar.Location = new System.Drawing.Point(2, 2);
            this.tBar.Name = "tBar";
            this.tBar.Padding = new System.Windows.Forms.Padding(0);
            this.tBar.Size = new System.Drawing.Size(848, 31);
            this.tBar.TabIndex = 1;
            this.tBar.Text = "Tools bar";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // txtShapeText
            // 
            this.txtShapeText.Name = "txtShapeText";
            this.txtShapeText.Size = new System.Drawing.Size(76, 31);
            this.txtShapeText.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // mnuMain
            // 
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mnuMain.Size = new System.Drawing.Size(860, 24);
            this.mnuMain.TabIndex = 2;
            this.mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMainQuit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // mnuMainQuit
            // 
            this.mnuMainQuit.Name = "mnuMainQuit";
            this.mnuMainQuit.Size = new System.Drawing.Size(97, 22);
            this.mnuMainQuit.Text = "&Quit";
            // 
            // sBar
            // 
            this.sBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.sBar.Location = new System.Drawing.Point(0, 470);
            this.sBar.Name = "sBar";
            this.sBar.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.sBar.Size = new System.Drawing.Size(860, 22);
            this.sBar.TabIndex = 4;
            this.sBar.Text = "statusStrip1";
            // 
            // tcFrame
            // 
            this.tcFrame.Controls.Add(this.tPageMainFrame);
            this.tcFrame.Controls.Add(this.tPageFrames);
            this.tcFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFrame.Location = new System.Drawing.Point(0, 24);
            this.tcFrame.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tcFrame.Name = "tcFrame";
            this.tcFrame.SelectedIndex = 0;
            this.tcFrame.Size = new System.Drawing.Size(860, 446);
            this.tcFrame.TabIndex = 11;
            // 
            // tPageMainFrame
            // 
            this.tPageMainFrame.Controls.Add(this.frameMain);
            this.tPageMainFrame.Controls.Add(this.splitter2);
            this.tPageMainFrame.Controls.Add(this.dgvShapeViews);
            this.tPageMainFrame.Controls.Add(this.splitter1);
            this.tPageMainFrame.Controls.Add(this.trvCameras);
            this.tPageMainFrame.Controls.Add(this.tBar);
            this.tPageMainFrame.Location = new System.Drawing.Point(4, 22);
            this.tPageMainFrame.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tPageMainFrame.Name = "tPageMainFrame";
            this.tPageMainFrame.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tPageMainFrame.Size = new System.Drawing.Size(852, 420);
            this.tPageMainFrame.TabIndex = 0;
            this.tPageMainFrame.Text = "Main Frame";
            this.tPageMainFrame.UseVisualStyleBackColor = true;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(114, 315);
            this.splitter2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(736, 2);
            this.splitter2.TabIndex = 9;
            this.splitter2.TabStop = false;
            // 
            // dgvShapeViews
            // 
            this.dgvShapeViews.AllowUserToAddRows = false;
            this.dgvShapeViews.AllowUserToDeleteRows = false;
            this.dgvShapeViews.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvShapeViews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShapeViews.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvShapeViews.Location = new System.Drawing.Point(114, 317);
            this.dgvShapeViews.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvShapeViews.Name = "dgvShapeViews";
            this.dgvShapeViews.RowHeadersVisible = false;
            this.dgvShapeViews.RowTemplate.Height = 24;
            this.dgvShapeViews.Size = new System.Drawing.Size(736, 101);
            this.dgvShapeViews.TabIndex = 10;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(112, 33);
            this.splitter1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 385);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // tPageFrames
            // 
            this.tPageFrames.Location = new System.Drawing.Point(4, 22);
            this.tPageFrames.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tPageFrames.Name = "tPageFrames";
            this.tPageFrames.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tPageFrames.Size = new System.Drawing.Size(852, 420);
            this.tPageFrames.TabIndex = 1;
            this.tPageFrames.Text = "Frames";
            this.tPageFrames.UseVisualStyleBackColor = true;
            // 
            // frameMain
            // 
            this.frameMain.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.frameMain.Camera = null;
            this.frameMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameMain.IsRotating = false;
            this.frameMain.Location = new System.Drawing.Point(114, 33);
            this.frameMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.frameMain.Name = "frameMain";
            this.frameMain.Size = new System.Drawing.Size(736, 282);
            this.frameMain.TabIndex = 6;
            this.frameMain.TextVisible = false;
            // 
            // btnRecord
            // 
            this.btnRecord.Image = global::DkVision.Properties.Resources.p32_camera_video;
            this.btnRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(72, 28);
            this.btnRecord.Text = "Record";
            // 
            // btnAutoAdjustment
            // 
            this.btnAutoAdjustment.Image = global::DkVision.Properties.Resources.p32_image_setting;
            this.btnAutoAdjustment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAutoAdjustment.Name = "btnAutoAdjustment";
            this.btnAutoAdjustment.Size = new System.Drawing.Size(69, 28);
            this.btnAutoAdjustment.Text = "Adjust";
            this.btnAutoAdjustment.Visible = false;
            // 
            // btnDrawRectangle
            // 
            this.btnDrawRectangle.Image = global::DkVision.Properties.Resources.p32_rectangle_round;
            this.btnDrawRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrawRectangle.Name = "btnDrawRectangle";
            this.btnDrawRectangle.Size = new System.Drawing.Size(87, 28);
            this.btnDrawRectangle.Tag = "0";
            this.btnDrawRectangle.Text = "Rectangle";
            // 
            // btnDrawCirlce
            // 
            this.btnDrawCirlce.Image = global::DkVision.Properties.Resources.p32_circle_color;
            this.btnDrawCirlce.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrawCirlce.Name = "btnDrawCirlce";
            this.btnDrawCirlce.Size = new System.Drawing.Size(65, 28);
            this.btnDrawCirlce.Tag = "1";
            this.btnDrawCirlce.Text = "Circle";
            // 
            // btnDrawPolygon
            // 
            this.btnDrawPolygon.Image = global::DkVision.Properties.Resources.p32_hexagon;
            this.btnDrawPolygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrawPolygon.Name = "btnDrawPolygon";
            this.btnDrawPolygon.Size = new System.Drawing.Size(79, 28);
            this.btnDrawPolygon.Tag = "2";
            this.btnDrawPolygon.Text = "Polygon";
            // 
            // btnRotate
            // 
            this.btnRotate.CheckOnClick = true;
            this.btnRotate.Image = global::DkVision.Properties.Resources.p32_rotate;
            this.btnRotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(69, 28);
            this.btnRotate.Text = "Rotate";
            this.btnRotate.CheckedChanged += new System.EventHandler(this.BtnRotate_CheckedChanged);
            // 
            // btnShowText
            // 
            this.btnShowText.CheckOnClick = true;
            this.btnShowText.Image = global::DkVision.Properties.Resources.p32_pencil;
            this.btnShowText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowText.Name = "btnShowText";
            this.btnShowText.Size = new System.Drawing.Size(88, 28);
            this.btnShowText.Text = "Show Text";
            this.btnShowText.CheckedChanged += new System.EventHandler(this.BtnShowText_CheckedChanged);
            // 
            // btnDrawConfirm
            // 
            this.btnDrawConfirm.Image = global::DkVision.Properties.Resources.p32_success;
            this.btnDrawConfirm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrawConfirm.Name = "btnDrawConfirm";
            this.btnDrawConfirm.Size = new System.Drawing.Size(79, 28);
            this.btnDrawConfirm.Text = "Confirm";
            this.btnDrawConfirm.Visible = false;
            // 
            // btnDrawCancel
            // 
            this.btnDrawCancel.Image = global::DkVision.Properties.Resources.p32_error;
            this.btnDrawCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrawCancel.Name = "btnDrawCancel";
            this.btnDrawCancel.Size = new System.Drawing.Size(71, 28);
            this.btnDrawCancel.Text = "Cancel";
            this.btnDrawCancel.Visible = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 492);
            this.Controls.Add(this.tcFrame);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.sBar);
            this.MainMenuStrip = this.mnuMain;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmMain";
            this.Text = "DkVision";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tBar.ResumeLayout(false);
            this.tBar.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.tcFrame.ResumeLayout(false);
            this.tPageMainFrame.ResumeLayout(false);
            this.tPageMainFrame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShapeViews)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvCameras;
        private System.Windows.Forms.ToolStrip tBar;
        private System.Windows.Forms.ToolStripButton btnDrawRectangle;
        private System.Windows.Forms.ToolStripButton btnDrawCirlce;
        private System.Windows.Forms.ToolStripButton btnDrawPolygon;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuMainQuit;
        private System.Windows.Forms.StatusStrip sBar;
        private UserControls.UcFrame frameMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDrawConfirm;
        private System.Windows.Forms.ToolStripButton btnDrawCancel;
        private System.Windows.Forms.ToolStripButton btnRecord;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox txtShapeText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnShowText;
        private System.Windows.Forms.TabControl tcFrame;
        private System.Windows.Forms.TabPage tPageMainFrame;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.DataGridView dgvShapeViews;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabPage tPageFrames;
        private System.Windows.Forms.ToolStripButton btnRotate;
        private System.Windows.Forms.ToolStripButton btnAutoAdjustment;
    }
}