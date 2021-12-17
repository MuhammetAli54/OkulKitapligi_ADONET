using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulKitapligi_ADONET
{
    public partial class FormGiris : Form
    {
        public FormGiris()
        {
            InitializeComponent();
        }

        private void FormGiris_Load(object sender, EventArgs e)
        {
            uC_MyButton_FormKitaplar.myButton.Text = "Kitap İşlemleri";
            uC_MyButton_FormKitaplar.myButton.Click += new EventHandler(btn_FormKitaplar);
        }

        private void btn_FormKitaplar(object sender, EventArgs e)
        {
            FormKitaplar frmKitap = new FormKitaplar();
            this.Hide();
            frmKitap.Show();
        }
    }
}
