namespace Schools_Management_System.School_Board
{
    partial class ShowStudentsImageFloat
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
            this.panelFloatingStudentImage = new System.Windows.Forms.Panel();
            this.tableLayoutPanelFloatingStudentImage = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxStudentImageShow = new System.Windows.Forms.PictureBox();
            this.panelFloatingStudentImage.SuspendLayout();
            this.tableLayoutPanelFloatingStudentImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStudentImageShow)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFloatingStudentImage
            // 
            this.panelFloatingStudentImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelFloatingStudentImage.Controls.Add(this.tableLayoutPanelFloatingStudentImage);
            this.panelFloatingStudentImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFloatingStudentImage.Location = new System.Drawing.Point(0, 0);
            this.panelFloatingStudentImage.Name = "panelFloatingStudentImage";
            this.panelFloatingStudentImage.Size = new System.Drawing.Size(384, 274);
            this.panelFloatingStudentImage.TabIndex = 75;
            // 
            // tableLayoutPanelFloatingStudentImage
            // 
            this.tableLayoutPanelFloatingStudentImage.ColumnCount = 3;
            this.tableLayoutPanelFloatingStudentImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.083333F));
            this.tableLayoutPanelFloatingStudentImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 96.09375F));
            this.tableLayoutPanelFloatingStudentImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.083333F));
            this.tableLayoutPanelFloatingStudentImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFloatingStudentImage.Controls.Add(this.pictureBoxStudentImageShow, 1, 1);
            this.tableLayoutPanelFloatingStudentImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFloatingStudentImage.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelFloatingStudentImage.Name = "tableLayoutPanelFloatingStudentImage";
            this.tableLayoutPanelFloatingStudentImage.RowCount = 3;
            this.tableLayoutPanelFloatingStudentImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.284672F));
            this.tableLayoutPanelFloatingStudentImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.79562F));
            this.tableLayoutPanelFloatingStudentImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.919708F));
            this.tableLayoutPanelFloatingStudentImage.Size = new System.Drawing.Size(384, 274);
            this.tableLayoutPanelFloatingStudentImage.TabIndex = 0;
            // 
            // pictureBoxStudentImageShow
            // 
            this.pictureBoxStudentImageShow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxStudentImageShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxStudentImageShow.Location = new System.Drawing.Point(10, 12);
            this.pictureBoxStudentImageShow.Name = "pictureBoxStudentImageShow";
            this.pictureBoxStudentImageShow.Size = new System.Drawing.Size(362, 251);
            this.pictureBoxStudentImageShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxStudentImageShow.TabIndex = 0;
            this.pictureBoxStudentImageShow.TabStop = false;
            // 
            // ShowStudentsImageFloat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 274);
            this.Controls.Add(this.panelFloatingStudentImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximumSize = new System.Drawing.Size(400, 500);
            this.Name = "ShowStudentsImageFloat";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panelFloatingStudentImage.ResumeLayout(false);
            this.tableLayoutPanelFloatingStudentImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStudentImageShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFloatingStudentImage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFloatingStudentImage;
        public System.Windows.Forms.PictureBox pictureBoxStudentImageShow;
    }
}