namespace DkVision.UI
{
    partial class FrmSetMask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetMask));
            this.propGridFilter = new System.Windows.Forms.PropertyGrid();
            this.lsbFilters = new System.Windows.Forms.ListBox();
            this.tlpLayout = new System.Windows.Forms.TableLayoutPanel();
            this.grbFilters = new System.Windows.Forms.GroupBox();
            this.btnNone = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.grbName = new System.Windows.Forms.GroupBox();
            this.txtFilterName = new System.Windows.Forms.TextBox();
            this.lblFilterName = new System.Windows.Forms.Label();
            this.tlpLayout.SuspendLayout();
            this.grbFilters.SuspendLayout();
            this.grbName.SuspendLayout();
            this.SuspendLayout();
            // 
            // propGridFilter
            // 
            this.propGridFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propGridFilter.Location = new System.Drawing.Point(225, 42);
            this.propGridFilter.Name = "propGridFilter";
            this.propGridFilter.Size = new System.Drawing.Size(875, 430);
            this.propGridFilter.TabIndex = 1;
            // 
            // lsbFilters
            // 
            this.lsbFilters.Dock = System.Windows.Forms.DockStyle.Left;
            this.lsbFilters.FormattingEnabled = true;
            this.lsbFilters.ItemHeight = 16;
            this.lsbFilters.Location = new System.Drawing.Point(3, 18);
            this.lsbFilters.Name = "lsbFilters";
            this.lsbFilters.Size = new System.Drawing.Size(222, 454);
            this.lsbFilters.TabIndex = 0;
            // 
            // tlpLayout
            // 
            this.tlpLayout.ColumnCount = 3;
            this.tlpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tlpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpLayout.Controls.Add(this.grbFilters, 0, 1);
            this.tlpLayout.Controls.Add(this.btnNone, 0, 2);
            this.tlpLayout.Controls.Add(this.btnRectangle, 1, 2);
            this.tlpLayout.Controls.Add(this.btnCircle, 2, 2);
            this.tlpLayout.Controls.Add(this.grbName, 0, 0);
            this.tlpLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpLayout.Name = "tlpLayout";
            this.tlpLayout.RowCount = 3;
            this.tlpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpLayout.Size = new System.Drawing.Size(1109, 605);
            this.tlpLayout.TabIndex = 2;
            // 
            // grbFilters
            // 
            this.tlpLayout.SetColumnSpan(this.grbFilters, 3);
            this.grbFilters.Controls.Add(this.propGridFilter);
            this.grbFilters.Controls.Add(this.lblFilterName);
            this.grbFilters.Controls.Add(this.lsbFilters);
            this.grbFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbFilters.Location = new System.Drawing.Point(3, 57);
            this.grbFilters.Name = "grbFilters";
            this.grbFilters.Size = new System.Drawing.Size(1103, 475);
            this.grbFilters.TabIndex = 1;
            this.grbFilters.TabStop = false;
            this.grbFilters.Text = "Filters";
            // 
            // btnNone
            // 
            this.btnNone.BackColor = System.Drawing.SystemColors.Control;
            this.btnNone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNone.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNone.Image = ((System.Drawing.Image)(resources.GetObject("btnNone.Image")));
            this.btnNone.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNone.Location = new System.Drawing.Point(3, 538);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(363, 64);
            this.btnNone.TabIndex = 2;
            this.btnNone.Text = "None";
            this.btnNone.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNone.UseVisualStyleBackColor = false;
            // 
            // btnRectangle
            // 
            this.btnRectangle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRectangle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRectangle.Image = global::DkVision.UI.Properties.Resources.p32_rectangle_round;
            this.btnRectangle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRectangle.Location = new System.Drawing.Point(372, 538);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(363, 64);
            this.btnRectangle.TabIndex = 3;
            this.btnRectangle.Text = "Rectangle";
            this.btnRectangle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRectangle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRectangle.UseVisualStyleBackColor = true;
            // 
            // btnCircle
            // 
            this.btnCircle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCircle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCircle.Image = global::DkVision.UI.Properties.Resources.p32_circle_color;
            this.btnCircle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCircle.Location = new System.Drawing.Point(741, 538);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(365, 64);
            this.btnCircle.TabIndex = 4;
            this.btnCircle.Text = "Circle";
            this.btnCircle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCircle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCircle.UseVisualStyleBackColor = true;
            // 
            // grbName
            // 
            this.tlpLayout.SetColumnSpan(this.grbName, 3);
            this.grbName.Controls.Add(this.txtFilterName);
            this.grbName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbName.Location = new System.Drawing.Point(3, 3);
            this.grbName.Name = "grbName";
            this.grbName.Size = new System.Drawing.Size(1103, 48);
            this.grbName.TabIndex = 0;
            this.grbName.TabStop = false;
            this.grbName.Text = "Name";
            // 
            // txtFilterName
            // 
            this.txtFilterName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilterName.Location = new System.Drawing.Point(3, 18);
            this.txtFilterName.Name = "txtFilterName";
            this.txtFilterName.Size = new System.Drawing.Size(1097, 22);
            this.txtFilterName.TabIndex = 0;
            // 
            // lblFilterName
            // 
            this.lblFilterName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFilterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterName.ForeColor = System.Drawing.Color.Blue;
            this.lblFilterName.Location = new System.Drawing.Point(225, 18);
            this.lblFilterName.Name = "lblFilterName";
            this.lblFilterName.Size = new System.Drawing.Size(875, 24);
            this.lblFilterName.TabIndex = 2;
            this.lblFilterName.Text = "None";
            this.lblFilterName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmSetMask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 605);
            this.Controls.Add(this.tlpLayout);
            this.Name = "FrmSetMask";
            this.Text = "Select filter";
            this.tlpLayout.ResumeLayout(false);
            this.grbFilters.ResumeLayout(false);
            this.grbName.ResumeLayout(false);
            this.grbName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PropertyGrid propGridFilter;
        private System.Windows.Forms.ListBox lsbFilters;
        private System.Windows.Forms.TableLayoutPanel tlpLayout;
        private System.Windows.Forms.GroupBox grbFilters;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.GroupBox grbName;
        private System.Windows.Forms.TextBox txtFilterName;
        private System.Windows.Forms.Label lblFilterName;
    }
}