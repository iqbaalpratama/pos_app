using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace pos_wpf
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        //SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\pos.mdf;Integrated Security=True;Connect Timeout=30");
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Salim\Documents\posdb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            loguser.Text = "";
            logpass.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(loguser.Text == "" || logpass.Text == "")
            {
                MessageBox.Show("PLEASE INSERT USERNAME AND PASSWWORD!");
            }
            if(logrole.Text == "ADMIN")
            {
                if(loguser.Text == "admin" && logpass.Text == "admin")
                {
                    Product prod = new Product();
                    prod.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username and or Password");
                }
            }
            else if(logrole.Text == "SELLER")
            {
                try
                {
                    Con.Open();
                    string query = "select count(8) from seller where SellerName = '" + loguser.Text +"' and SellerPass = '" + logpass.Text + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        Selling seli = new Selling(loguser.Text);
                        seli.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Username and or Password");
                    }
                    Con.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("PICK A ROLE FIRST!");
            }
        }
    }
}
