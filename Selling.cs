using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pos_wpf
{
    public partial class Selling : Form
    {
        public Selling(String user)
        {
            InitializeComponent();

        }

        public Selling()
        {
            InitializeComponent();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            PaymentMethod pm = new PaymentMethod();
            pm.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Show();
            this.Hide();
        }
    }
}
