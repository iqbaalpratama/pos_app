using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace pos_wpf
{
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Salim\Documents\posdb.mdf;Integrated Security=True;Connect Timeout=30");
        //SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\pos.mdf;Integrated Security=True;Connect Timeout=30");
        
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into Category values(" + catid.Text + ",'" + catname.Text + "','" + catdesc.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully");
                Con.Close();
                populate();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from category";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            catdgv.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Category_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void catdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            catid.Text = "belum bisa";
            catname.Text = "belum bisa";
            catdesc.Text = "belum bisa";
            //catid.Text = catdgv.SelectedRows[0].Cells[0].Value.ToString();
            //catname.Text = catdgv.SelectedRows[0].Cells[1].Value.ToString();
            //catdesc.Text = catdgv.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if(catid.Text == "")
                {
                    MessageBox.Show("Select A Category to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "delete from category where CatId = " + catid.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted Successfully");
                    Con.Close();
                    populate();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if(catid.Text == "" || catname.Text == "" || catdesc.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "update category set CatName = '" + catname.Text + "', CatDesc = '" + catdesc.Text + "' where CatId = " + catid.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Edited Successfully");
                    Con.Close();
                    populate();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Product prod = new Product();
            prod.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sellers sel = new Sellers();
            sel.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PaymentMethod pm = new PaymentMethod();
            pm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Selling sell = new Selling();
            sell.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginForm log = new LoginForm();
            log.Show();
            this.Hide();
        }
    }
}
