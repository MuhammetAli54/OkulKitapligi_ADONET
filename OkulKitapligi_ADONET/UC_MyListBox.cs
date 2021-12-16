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
    public partial class UC_MyListBox : UserControl
    {
        public UC_MyListBox()
        {
            InitializeComponent();
        }

        private void myTextBox_TextChanged(object sender, EventArgs e)
        {
            if (myTextBox.Text.Trim().Length > 2)
            {
                if (myListBox.Items.Count > 0)
                {
                    //to be continued...
                }

            }
        }
    }
}
