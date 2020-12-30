namespace Schools_Management_System.SchoolManagementForms.New_Windows_Form
{
    partial class SchoolBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchoolBoard));
            this.panelAddSaveEditDelBut = new System.Windows.Forms.Panel();
            this.iconButtonPrevious = new FontAwesome.Sharp.IconButton();
            this.iconButtonNext = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // panelChildFormContainer
            // 
            resources.ApplyResources(this.panelChildFormContainer, "panelChildFormContainer");
            // 
            // panelAddSaveEditDelBut
            // 
            this.panelAddSaveEditDelBut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            resources.ApplyResources(this.panelAddSaveEditDelBut, "panelAddSaveEditDelBut");
            this.panelAddSaveEditDelBut.Name = "panelAddSaveEditDelBut";
            // 
            // iconButtonPrevious
            // 
            this.iconButtonPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.iconButtonPrevious, "iconButtonPrevious");
            this.iconButtonPrevious.FlatAppearance.BorderSize = 0;
            this.iconButtonPrevious.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButtonPrevious.IconChar = FontAwesome.Sharp.IconChar.ChevronCircleLeft;
            this.iconButtonPrevious.IconColor = System.Drawing.Color.White;
            this.iconButtonPrevious.IconSize = 30;
            this.iconButtonPrevious.Name = "iconButtonPrevious";
            this.iconButtonPrevious.Rotation = 0D;
            this.iconButtonPrevious.UseVisualStyleBackColor = true;
            // 
            // iconButtonNext
            // 
            this.iconButtonNext.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.iconButtonNext, "iconButtonNext");
            this.iconButtonNext.FlatAppearance.BorderSize = 0;
            this.iconButtonNext.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconButtonNext.IconChar = FontAwesome.Sharp.IconChar.ChevronCircleRight;
            this.iconButtonNext.IconColor = System.Drawing.Color.White;
            this.iconButtonNext.IconSize = 30;
            this.iconButtonNext.Name = "iconButtonNext";
            this.iconButtonNext.Rotation = 0D;
            this.iconButtonNext.UseVisualStyleBackColor = true;
            // 
            // SchoolBoard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SchoolBoard";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelAddSaveEditDelBut;
        private FontAwesome.Sharp.IconButton iconButtonPrevious;
        private FontAwesome.Sharp.IconButton iconButtonNext;
    }
}