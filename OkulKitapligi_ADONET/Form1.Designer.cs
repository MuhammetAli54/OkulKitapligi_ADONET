﻿
namespace OkulKitapligi_ADONET
{
    partial class FormYazarlar
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtYazar = new System.Windows.Forms.TextBox();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnTemizle = new System.Windows.Forms.Button();
            this.dataGridViewYazarlar = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.guncelleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.silToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.silBaskaBirYontemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.silPasifeCekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewYazarlar)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Yazar Ad Soyad : ";
            // 
            // txtYazar
            // 
            this.txtYazar.Location = new System.Drawing.Point(221, 29);
            this.txtYazar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtYazar.Name = "txtYazar";
            this.txtYazar.Size = new System.Drawing.Size(465, 26);
            this.txtYazar.TabIndex = 1;
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(749, 25);
            this.btnEkle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(104, 32);
            this.btnEkle.TabIndex = 2;
            this.btnEkle.Text = "EKLE";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnTemizle
            // 
            this.btnTemizle.Location = new System.Drawing.Point(879, 25);
            this.btnTemizle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTemizle.Name = "btnTemizle";
            this.btnTemizle.Size = new System.Drawing.Size(104, 32);
            this.btnTemizle.TabIndex = 3;
            this.btnTemizle.Text = "TEMİZLE";
            this.btnTemizle.UseVisualStyleBackColor = true;
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);
            // 
            // dataGridViewYazarlar
            // 
            this.dataGridViewYazarlar.AllowUserToAddRows = false;
            this.dataGridViewYazarlar.AllowUserToDeleteRows = false;
            this.dataGridViewYazarlar.AllowUserToOrderColumns = true;
            this.dataGridViewYazarlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewYazarlar.Location = new System.Drawing.Point(65, 102);
            this.dataGridViewYazarlar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewYazarlar.Name = "dataGridViewYazarlar";
            this.dataGridViewYazarlar.ReadOnly = true;
            this.dataGridViewYazarlar.RowHeadersWidth = 51;
            this.dataGridViewYazarlar.RowTemplate.Height = 24;
            this.dataGridViewYazarlar.Size = new System.Drawing.Size(918, 524);
            this.dataGridViewYazarlar.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guncelleToolStripMenuItem,
            this.silToolStripMenuItem,
            this.silBaskaBirYontemToolStripMenuItem,
            this.silPasifeCekToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(223, 128);
            // 
            // guncelleToolStripMenuItem
            // 
            this.guncelleToolStripMenuItem.Name = "guncelleToolStripMenuItem";
            this.guncelleToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.guncelleToolStripMenuItem.Text = "Güncelle";
            this.guncelleToolStripMenuItem.Click += new System.EventHandler(this.guncelleToolStripMenuItem_Click);
            // 
            // silToolStripMenuItem
            // 
            this.silToolStripMenuItem.Name = "silToolStripMenuItem";
            this.silToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.silToolStripMenuItem.Text = "Sil";
            this.silToolStripMenuItem.Click += new System.EventHandler(this.silToolStripMenuItem_Click);
            // 
            // silBaskaBirYontemToolStripMenuItem
            // 
            this.silBaskaBirYontemToolStripMenuItem.Name = "silBaskaBirYontemToolStripMenuItem";
            this.silBaskaBirYontemToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.silBaskaBirYontemToolStripMenuItem.Text = "Sil - Başka bir yöntem";
            this.silBaskaBirYontemToolStripMenuItem.Click += new System.EventHandler(this.silBaskaBirYontemToolStripMenuItem_Click);
            // 
            // silPasifeCekToolStripMenuItem
            // 
            this.silPasifeCekToolStripMenuItem.Name = "silPasifeCekToolStripMenuItem";
            this.silPasifeCekToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.silPasifeCekToolStripMenuItem.Text = "Sil - Pasife Çek";
            this.silPasifeCekToolStripMenuItem.Click += new System.EventHandler(this.silPasifeCekToolStripMenuItem_Click);
            // 
            // FormYazarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1330, 661);
            this.Controls.Add(this.dataGridViewYazarlar);
            this.Controls.Add(this.btnTemizle);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.txtYazar);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormYazarlar";
            this.Text = "Yazar İşlemleri";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewYazarlar)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtYazar;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnTemizle;
        private System.Windows.Forms.DataGridView dataGridViewYazarlar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem guncelleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem silToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem silBaskaBirYontemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem silPasifeCekToolStripMenuItem;
    }
}

