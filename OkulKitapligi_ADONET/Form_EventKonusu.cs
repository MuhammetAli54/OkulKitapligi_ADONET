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
    public partial class Form_EventKonusu : Form
    {
        public Form_EventKonusu()
        {
            InitializeComponent();
        }

        private void btn_FareUzerineGelinceRenkDegissin(object sender, EventArgs e)
        {
            //button1.BackColor = Color.Pink;
            //button2.BackColor = Color.Pink;

            //Ya çok daha fazla button varsa?
            foreach (var item in this.Controls)
            {
                if (item is Button && ((Button)sender).Name==((Button)item).Name)
                {
                    ((Button)item).BackColor = Color.DarkCyan;
                }
            }
        }
        private void btn_FareAyrilincaDefaultRengeDonsun(object sender, EventArgs e)
        {
            //button1.BackColor = Color.Pink;
            //button2.BackColor = Color.Pink;

            //Ya çok daha fazla button varsa?
            foreach (var item in this.Controls)
            {
                if (item is Button && ((Button)sender).Name == ((Button)item).Name)
                {
                    ((Button)item).BackColor = SystemColors.Control;
                }
            }
            tabControl1.Click += new EventHandler(TabaTiklandi);
        }

        private void Form_EventKonusu_Load(object sender, EventArgs e)
        {
            //buttonların MouseLeave event'larını properties ekranındaki event sekmesinden değil form'un Load'ında kod ekranında biz verebiliriz.

            button1.MouseLeave += new EventHandler(btn_FareAyrilincaDefaultRengeDonsun);
            button2.MouseLeave += new EventHandler(btn_FareAyrilincaDefaultRengeDonsun);
            button3.MouseLeave += new EventHandler(btn_FareAyrilincaDefaultRengeDonsun);
        }
        private void TabaTiklandi(object sender, EventArgs e)
        {
            MessageBox.Show("tıkladın!!!");
        }
    }
}
