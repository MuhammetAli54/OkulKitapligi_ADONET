
namespace OkulKitapligi_ADONET
{
    partial class FormKitapOduncIslemleri
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
            this.components = new System.ComponentModel.Container();
            this.comboBoxOgrenci = new System.Windows.Forms.ComboBox();
            this.comboBoxKitap = new System.Windows.Forms.ComboBox();
            this.groupBoxOgrenci = new System.Windows.Forms.GroupBox();
            this.groupBoxKitap = new System.Windows.Forms.GroupBox();
            this.dataGridViewOduncKitaplar = new System.Windows.Forms.DataGridView();
            this.groupBoxOduncTarihler = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerBitis = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerBaslangic = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kitabiTesliEtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UC_MyButtonOduncAl = new OkulKitapligi_ADONET.UC_MyButton();
            this.groupBoxOgrenci.SuspendLayout();
            this.groupBoxKitap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOduncKitaplar)).BeginInit();
            this.groupBoxOduncTarihler.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxOgrenci
            // 
            this.comboBoxOgrenci.FormattingEnabled = true;
            this.comboBoxOgrenci.Location = new System.Drawing.Point(0, 32);
            this.comboBoxOgrenci.Name = "comboBoxOgrenci";
            this.comboBoxOgrenci.Size = new System.Drawing.Size(336, 24);
            this.comboBoxOgrenci.TabIndex = 0;
            this.comboBoxOgrenci.SelectedIndexChanged += new System.EventHandler(this.comboBoxOgrenci_SelectedIndexChanged);
            // 
            // comboBoxKitap
            // 
            this.comboBoxKitap.FormattingEnabled = true;
            this.comboBoxKitap.Location = new System.Drawing.Point(6, 31);
            this.comboBoxKitap.Name = "comboBoxKitap";
            this.comboBoxKitap.Size = new System.Drawing.Size(336, 24);
            this.comboBoxKitap.TabIndex = 1;
            this.comboBoxKitap.SelectedIndexChanged += new System.EventHandler(this.comboBoxKitap_SelectedIndexChanged);
            // 
            // groupBoxOgrenci
            // 
            this.groupBoxOgrenci.Controls.Add(this.comboBoxOgrenci);
            this.groupBoxOgrenci.Location = new System.Drawing.Point(21, 44);
            this.groupBoxOgrenci.Name = "groupBoxOgrenci";
            this.groupBoxOgrenci.Size = new System.Drawing.Size(348, 78);
            this.groupBoxOgrenci.TabIndex = 2;
            this.groupBoxOgrenci.TabStop = false;
            this.groupBoxOgrenci.Text = "Öğrenci Seçiniz:";
            // 
            // groupBoxKitap
            // 
            this.groupBoxKitap.Controls.Add(this.comboBoxKitap);
            this.groupBoxKitap.Location = new System.Drawing.Point(21, 142);
            this.groupBoxKitap.Name = "groupBoxKitap";
            this.groupBoxKitap.Size = new System.Drawing.Size(348, 75);
            this.groupBoxKitap.TabIndex = 3;
            this.groupBoxKitap.TabStop = false;
            this.groupBoxKitap.Text = "Kitap Seçiniz:";
            // 
            // dataGridViewOduncKitaplar
            // 
            this.dataGridViewOduncKitaplar.AllowUserToAddRows = false;
            this.dataGridViewOduncKitaplar.AllowUserToDeleteRows = false;
            this.dataGridViewOduncKitaplar.AllowUserToOrderColumns = true;
            this.dataGridViewOduncKitaplar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOduncKitaplar.Location = new System.Drawing.Point(27, 256);
            this.dataGridViewOduncKitaplar.Name = "dataGridViewOduncKitaplar";
            this.dataGridViewOduncKitaplar.ReadOnly = true;
            this.dataGridViewOduncKitaplar.RowHeadersWidth = 51;
            this.dataGridViewOduncKitaplar.RowTemplate.Height = 24;
            this.dataGridViewOduncKitaplar.Size = new System.Drawing.Size(1022, 350);
            this.dataGridViewOduncKitaplar.TabIndex = 2;
            // 
            // groupBoxOduncTarihler
            // 
            this.groupBoxOduncTarihler.Controls.Add(this.UC_MyButtonOduncAl);
            this.groupBoxOduncTarihler.Controls.Add(this.label2);
            this.groupBoxOduncTarihler.Controls.Add(this.dateTimePickerBitis);
            this.groupBoxOduncTarihler.Controls.Add(this.dateTimePickerBaslangic);
            this.groupBoxOduncTarihler.Controls.Add(this.label1);
            this.groupBoxOduncTarihler.Location = new System.Drawing.Point(404, 12);
            this.groupBoxOduncTarihler.Name = "groupBoxOduncTarihler";
            this.groupBoxOduncTarihler.Size = new System.Drawing.Size(369, 224);
            this.groupBoxOduncTarihler.TabIndex = 4;
            this.groupBoxOduncTarihler.TabStop = false;
            this.groupBoxOduncTarihler.Text = "Tarihleri Seçiniz:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bitiş Tarihi:";
            // 
            // dateTimePickerBitis
            // 
            this.dateTimePickerBitis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerBitis.Location = new System.Drawing.Point(20, 127);
            this.dateTimePickerBitis.Name = "dateTimePickerBitis";
            this.dateTimePickerBitis.Size = new System.Drawing.Size(332, 22);
            this.dateTimePickerBitis.TabIndex = 2;
            // 
            // dateTimePickerBaslangic
            // 
            this.dateTimePickerBaslangic.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerBaslangic.Location = new System.Drawing.Point(20, 62);
            this.dateTimePickerBaslangic.Name = "dateTimePickerBaslangic";
            this.dateTimePickerBaslangic.Size = new System.Drawing.Size(332, 22);
            this.dateTimePickerBaslangic.TabIndex = 1;
            this.dateTimePickerBaslangic.ValueChanged += new System.EventHandler(this.dateTimePickerBaslangic_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Başlangıç tarihi:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kitabiTesliEtToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 28);
            // 
            // kitabiTesliEtToolStripMenuItem
            // 
            this.kitabiTesliEtToolStripMenuItem.Name = "kitabiTesliEtToolStripMenuItem";
            this.kitabiTesliEtToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.kitabiTesliEtToolStripMenuItem.Text = "Kitabı Teslim Et";
            this.kitabiTesliEtToolStripMenuItem.Click += new System.EventHandler(this.kitabiTeslimEtToolStripMenuItem_Click);
            // 
            // UC_MyButtonOduncAl
            // 
            this.UC_MyButtonOduncAl.Location = new System.Drawing.Point(20, 169);
            this.UC_MyButtonOduncAl.Name = "UC_MyButtonOduncAl";
            this.UC_MyButtonOduncAl.Size = new System.Drawing.Size(329, 36);
            this.UC_MyButtonOduncAl.TabIndex = 4;
            // 
            // FormKitapOduncIslemleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 668);
            this.Controls.Add(this.groupBoxOduncTarihler);
            this.Controls.Add(this.dataGridViewOduncKitaplar);
            this.Controls.Add(this.groupBoxKitap);
            this.Controls.Add(this.groupBoxOgrenci);
            this.Name = "FormKitapOduncIslemleri";
            this.Text = "KİTAP ÖDÜNÇ ALIM FORMU";
            this.Load += new System.EventHandler(this.FormKitapOduncIslemleri_Load);
            this.groupBoxOgrenci.ResumeLayout(false);
            this.groupBoxKitap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOduncKitaplar)).EndInit();
            this.groupBoxOduncTarihler.ResumeLayout(false);
            this.groupBoxOduncTarihler.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxOgrenci;
        private System.Windows.Forms.ComboBox comboBoxKitap;
        private System.Windows.Forms.GroupBox groupBoxOgrenci;
        private System.Windows.Forms.GroupBox groupBoxKitap;
        private System.Windows.Forms.DataGridView dataGridViewOduncKitaplar;
        private System.Windows.Forms.GroupBox groupBoxOduncTarihler;
        private System.Windows.Forms.DateTimePicker dateTimePickerBitis;
        private System.Windows.Forms.DateTimePicker dateTimePickerBaslangic;
        private System.Windows.Forms.Label label1;
        private UC_MyButton UC_MyButtonOduncAl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem kitabiTesliEtToolStripMenuItem;
    }
}