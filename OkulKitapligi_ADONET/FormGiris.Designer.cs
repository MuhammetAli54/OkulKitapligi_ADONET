
namespace OkulKitapligi_ADONET
{
    partial class FormGiris
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
            this.uC_MyButton_FormKitaplar = new OkulKitapligi_ADONET.UC_MyButton();
            this.uC_MyButton2 = new OkulKitapligi_ADONET.UC_MyButton();
            this.SuspendLayout();
            // 
            // uC_MyButton_FormKitaplar
            // 
            this.uC_MyButton_FormKitaplar.Location = new System.Drawing.Point(105, 63);
            this.uC_MyButton_FormKitaplar.Name = "uC_MyButton_FormKitaplar";
            this.uC_MyButton_FormKitaplar.Size = new System.Drawing.Size(159, 58);
            this.uC_MyButton_FormKitaplar.TabIndex = 0;
            // 
            // uC_MyButton2
            // 
            this.uC_MyButton2.Location = new System.Drawing.Point(105, 249);
            this.uC_MyButton2.Name = "uC_MyButton2";
            this.uC_MyButton2.Size = new System.Drawing.Size(159, 63);
            this.uC_MyButton2.TabIndex = 1;
            // 
            // FormGiris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 601);
            this.Controls.Add(this.uC_MyButton2);
            this.Controls.Add(this.uC_MyButton_FormKitaplar);
            this.Name = "FormGiris";
            this.Text = "HOŞGELDİNİZ...";
            this.Load += new System.EventHandler(this.FormGiris_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UC_MyButton uC_MyButton_FormKitaplar;
        private UC_MyButton uC_MyButton2;
    }
}