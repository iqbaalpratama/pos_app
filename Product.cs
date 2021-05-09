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
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\pos.mdf;Integrated Security=True;Connect Timeout=30");

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void fillcombo()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CatName from category",Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            prodcat.ValueMember = "catName";
            prodcat.DataSource = dt;

            Con.Close();
        }
        private void populate()
        {
            Con.Open();
            string query = "select * from Prod";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            proddgv.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            fillcombo();
            populate();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into Prod values(" + prodid.Text + ",'" + prodname.Text + "'," + prodqty.Text + ",'"+ prodcat.Text + "',"+prodout.Text+ ","+ prodprice.Text+")";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully");
                Con.Close();
                populate();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodid.Text == "")
                {
                    MessageBox.Show("Select A Product to Delete");
                }
                else
                {
                    Con.Open();
                    string query = "delete from Prod where ProdId = " + prodid.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted Successfully");
                    Con.Close();
                    populate();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodid.Text == "" || prodname.Text == "" || prodqty.Text == "" || prodprice.Text == "" || prodout.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "update Prod set ProdName = '" + prodname.Text + "', ProdQty = " + prodqty.Text + ", ProdPriceSell = " + prodprice.Text + ", ProdCat = '"+ prodcat.Text +"', ProdPriceBuy =" + prodout.Text + " where ProdId = " + prodid.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Edited Successfully");
                    Con.Close();
                    populate();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sellers sel = new Sellers();
            sel.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Category cat = new Category();
            cat.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PaymentMethod pm = new PaymentMethod();
            pm.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string jumlah = "";
            string outcome = "";
            try
            {
                if (prodid.Text == "" || prodname.Text == "" || prodqty.Text == "" || prodprice.Text == "" || prodout.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    if (prodid.Text != "")
                    {
                        string queryselect = "select * from Prod WHERE ProdId = " + prodid.Text + "";
                        SqlCommand cmd1 = new SqlCommand(queryselect, Con);
                        SqlDataReader hasil = cmd1.ExecuteReader();
                        {
                                if (!hasil.HasRows)
                                {
                                    string msg  = "No Product with that ID";
                                    MessageBox.Show(msg);
                                }
                                else
                                {
                                    hasil.Read();
                                    jumlah = hasil["ProdQty"].ToString();
                                    outcome = hasil["ProdPriceBuy"].ToString();
                                }
                        }
                        Con.Close();
                        Con.Open();
                        string query = "update Prod set ProdName = '" + prodname.Text + "', ProdQty = " + (int.Parse(prodqty.Text) + int.Parse(jumlah)) + ", ProdPriceSell = " + prodprice.Text + ", ProdCat = '" + prodcat.Text + "', ProdPriceBuy =" + (int.Parse(prodout.Text) + int.Parse(outcome)) + " where ProdId = " + prodid.Text + "";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Product Edited Successfully");
                        Con.Close();
                        populate();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
