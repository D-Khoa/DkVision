﻿namespace DkVision.UI
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
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.btnRefreshCameraList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCapture = new System.Windows.Forms.ToolStripButton();
            this.btnLive = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.sbtnDrawTool = new System.Windows.Forms.ToolStripSplitButton();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDrawLineTool = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDrawRectTool = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDrawCircleTool = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFilter = new System.Windows.Forms.ToolStripButton();
            this.trvCamera = new System.Windows.Forms.TreeView();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuiBtnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.sttBar = new System.Windows.Forms.StatusStrip();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ucMainFrame = new DkVision.UI.Components.UcFrame();
            this.pnlMasks = new System.Windows.Forms.Panel();
            this.tlpMainLayout.SuspendLayout();
            this.toolBar.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.sttBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 2;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Controls.Add(this.toolBar, 0, 0);
            this.tlpMainLayout.Controls.Add(this.trvCamera, 0, 1);
            this.tlpMainLayout.Controls.Add(this.ucMainFrame, 1, 1);
            this.tlpMainLayout.Controls.Add(this.pnlMasks, 0, 2);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(0, 30);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 3;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpMainLayout.Size = new System.Drawing.Size(1086, 609);
            this.tlpMainLayout.TabIndex = 2;
            // 
            // toolBar
            // 
            this.tlpMainLayout.SetColumnSpan(this.toolBar, 2);
            this.toolBar.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefreshCameraList,
            this.toolStripSeparator2,
            this.btnCapture,
            this.btnLive,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.sbtnDrawTool,
            this.btnFilter});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(1086, 39);
            this.toolBar.TabIndex = 5;
            this.toolBar.Text = "Draw tools";
            // 
            // btnRefreshCameraList
            // 
            this.btnRefreshCameraList.Image = global::DkVision.UI.Properties.Resources.p32_refresh;
            this.btnRefreshCameraList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshCameraList.Name = "btnRefreshCameraList";
            this.btnRefreshCameraList.Size = new System.Drawing.Size(94, 36);
            this.btnRefreshCameraList.Text = "Refresh";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // btnCapture
            // 
            this.btnCapture.Image = global::DkVision.UI.Properties.Resources.p32_camera;
            this.btnCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(97, 36);
            this.btnCapture.Text = "Capture";
            // 
            // btnLive
            // 
            this.btnLive.Image = global::DkVision.UI.Properties.Resources.p32_camera_video;
            this.btnLive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(71, 36);
            this.btnLive.Text = "Live";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(53, 36);
            this.toolStripLabel2.Text = "Shape:";
            this.toolStripLabel2.Visible = false;
            // 
            // sbtnDrawTool
            // 
            this.sbtnDrawTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.btnDrawLineTool,
            this.btnDrawRectTool,
            this.btnDrawCircleTool});
            this.sbtnDrawTool.Image = global::DkVision.UI.Properties.Resources.p32_no_pic;
            this.sbtnDrawTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbtnDrawTool.Name = "sbtnDrawTool";
            this.sbtnDrawTool.Size = new System.Drawing.Size(96, 36);
            this.sbtnDrawTool.Text = "None";
            this.sbtnDrawTool.Visible = false;
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Image = global::DkVision.UI.Properties.Resources.p32_no_pic;
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.noneToolStripMenuItem.Tag = "0";
            this.noneToolStripMenuItem.Text = "None";
            // 
            // btnDrawLineTool
            // 
            this.btnDrawLineTool.Image = global::DkVision.UI.Properties.Resources.p32_line_color;
            this.btnDrawLineTool.Name = "btnDrawLineTool";
            this.btnDrawLineTool.Size = new System.Drawing.Size(158, 26);
            this.btnDrawLineTool.Tag = "1";
            this.btnDrawLineTool.Text = "Line";
            // 
            // btnDrawRectTool
            // 
            this.btnDrawRectTool.Image = global::DkVision.UI.Properties.Resources.p32_rectangle_round;
            this.btnDrawRectTool.Name = "btnDrawRectTool";
            this.btnDrawRectTool.Size = new System.Drawing.Size(158, 26);
            this.btnDrawRectTool.Tag = "2";
            this.btnDrawRectTool.Text = "Rectangle";
            // 
            // btnDrawCircleTool
            // 
            this.btnDrawCircleTool.Image = global::DkVision.UI.Properties.Resources.p32_circle_color;
            this.btnDrawCircleTool.Name = "btnDrawCircleTool";
            this.btnDrawCircleTool.Size = new System.Drawing.Size(158, 26);
            this.btnDrawCircleTool.Tag = "3";
            this.btnDrawCircleTool.Text = "Circle";
            // 
            // btnFilter
            // 
            this.btnFilter.Image = global::DkVision.UI.Properties.Resources.p32_pencil;
            this.btnFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(78, 36);
            this.btnFilter.Text = "Filter";
            // 
            // trvCamera
            // 
            this.trvCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCamera.Location = new System.Drawing.Point(3, 42);
            this.trvCamera.Name = "trvCamera";
            this.trvCamera.Size = new System.Drawing.Size(194, 364);
            this.trvCamera.TabIndex = 8;
            // 
            // mnuMain
            // 
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1086, 30);
            this.mnuMain.TabIndex = 3;
            this.mnuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuiBtnExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // mnuiBtnExit
            // 
            this.mnuiBtnExit.Name = "mnuiBtnExit";
            this.mnuiBtnExit.Size = new System.Drawing.Size(116, 26);
            this.mnuiBtnExit.Text = "Exit";
            this.mnuiBtnExit.Click += new System.EventHandler(this.MnuiBtnExit_Click);
            // 
            // sttBar
            // 
            this.sttBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.sttBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatus});
            this.sttBar.Location = new System.Drawing.Point(0, 639);
            this.sttBar.Name = "sttBar";
            this.sttBar.Size = new System.Drawing.Size(1086, 26);
            this.sttBar.TabIndex = 4;
            this.sttBar.Text = "statusStrip1";
            // 
            // lbStatus
            // 
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(45, 20);
            this.lbStatus.Text = "None";
            // 
            // ucMainFrame
            // 
            this.ucMainFrame.BackColor = System.Drawing.Color.White;
            this.ucMainFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ucMainFrame.Camera = null;
            this.ucMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMainFrame.IsAuto = false;
            this.ucMainFrame.Location = new System.Drawing.Point(203, 42);
            this.ucMainFrame.Name = "ucMainFrame";
            this.ucMainFrame.Size = new System.Drawing.Size(880, 364);
            this.ucMainFrame.TabIndex = 9;
            // 
            // pnlMasks
            // 
            this.tlpMainLayout.SetColumnSpan(this.pnlMasks, 2);
            this.pnlMasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMasks.Location = new System.Drawing.Point(3, 412);
            this.pnlMasks.Name = "pnlMasks";
            this.pnlMasks.Size = new System.Drawing.Size(1080, 194);
            this.pnlMasks.TabIndex = 10;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 665);
            this.Controls.Add(this.tlpMainLayout);
            this.Controls.Add(this.sttBar);
            this.Controls.Add(this.mnuMain);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "FrmMain";
            this.Text = "Camera Vision";
            this.tlpMainLayout.ResumeLayout(false);
            this.tlpMainLayout.PerformLayout();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.sttBar.ResumeLayout(false);
            this.sttBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuiBtnExit;
        private System.Windows.Forms.ToolStripSplitButton sbtnDrawTool;
        private System.Windows.Forms.ToolStripMenuItem btnDrawRectTool;
        private System.Windows.Forms.ToolStripMenuItem btnDrawLineTool;
        private System.Windows.Forms.ToolStripMenuItem btnDrawCircleTool;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.StatusStrip sttBar;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.ToolStripButton btnLive;
        private System.Windows.Forms.ToolStripButton btnCapture;
        private System.Windows.Forms.ToolStripButton btnRefreshCameraList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnFilter;
        private System.Windows.Forms.TreeView trvCamera;
        private Components.UcFrame ucMainFrame;
        private System.Windows.Forms.Panel pnlMasks;
    }
}