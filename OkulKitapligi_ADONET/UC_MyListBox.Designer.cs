
namespace OkulKitapligi_ADONET
{
    partial class UC_MyListBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.myTextBox = new System.Windows.Forms.TextBox();
            this.myListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // myTextBox
            // 
            this.myTextBox.Location = new System.Drawing.Point(18, 26);
            this.myTextBox.Name = "myTextBox";
            this.myTextBox.Size = new System.Drawing.Size(287, 22);
            this.myTextBox.TabIndex = 0;
            this.myTextBox.TextChanged += new System.EventHandler(this.myTextBox_TextChanged);
            // 
            // myListBox
            // 
            this.myListBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.myListBox.FormattingEnabled = true;
            this.myListBox.ItemHeight = 16;
            this.myListBox.Location = new System.Drawing.Point(0, 67);
            this.myListBox.Name = "myListBox";
            this.myListBox.Size = new System.Drawing.Size(324, 548);
            this.myListBox.TabIndex = 1;
            // 
            // UC_MyListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.myListBox);
            this.Controls.Add(this.myTextBox);
            this.Name = "UC_MyListBox";
            this.Size = new System.Drawing.Size(324, 615);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox myTextBox;
        public System.Windows.Forms.ListBox myListBox;
    }
}
