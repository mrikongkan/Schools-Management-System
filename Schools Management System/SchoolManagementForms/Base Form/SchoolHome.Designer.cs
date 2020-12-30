namespace Schools_Management_System.SchoolManagementForms.New_Windows_Form
{
    partial class SchoolHome
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
            this.panelChildFormContainer = new System.Windows.Forms.Panel();
            this.panelSchollHomeMother = new System.Windows.Forms.Panel();
            this.panelSchollHomeMother.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChildFormContainer
            // 
            this.panelChildFormContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelChildFormContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildFormContainer.Location = new System.Drawing.Point(0, 0);
            this.panelChildFormContainer.Name = "panelChildFormContainer";
            this.panelChildFormContainer.Size = new System.Drawing.Size(800, 450);
            this.panelChildFormContainer.TabIndex = 4;
            // 
            // panelSchollHomeMother
            // 
            this.panelSchollHomeMother.Controls.Add(this.panelChildFormContainer);
            this.panelSchollHomeMother.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSchollHomeMother.Location = new System.Drawing.Point(0, 0);
            this.panelSchollHomeMother.Name = "panelSchollHomeMother";
            this.panelSchollHomeMother.Size = new System.Drawing.Size(800, 450);
            this.panelSchollHomeMother.TabIndex = 4;
            // 
            // SchoolHome
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelSchollHomeMother);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.IsMdiContainer = true;
            this.Name = "SchoolHome";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SchoolHome_Load);
            this.panelSchollHomeMother.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelChildFormContainer;
        private System.Windows.Forms.Panel panelSchollHomeMother;
    }
}