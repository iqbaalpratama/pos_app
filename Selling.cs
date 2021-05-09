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
    public partial class Selling : Form
    {
        string user = "";
        public Selling(String user)
        {
            InitializeComponent();
            this.user = user;
        }

        public Selling()
        {
            InitializeComponent();
            this.user = "Admin";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PaymentMethod pm = new PaymentMethod();
            pm.Show();
            this.Hide();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Salim\Documents\posdb.mdf;Integrated Security=True;Connect Timeout=30");

        private void fillcat()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CatName from category", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            selprodcat.ValueMember = "catName";
            selprodcat.DataSource = dt;
            Con.Close();
        }

        private void fillpay()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select PaymentName from payment", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PaymentName", typeof(string));
            dt.Load(rdr);
            selpay.ValueMember = "paymentName";
            selpay.DataSource = dt;
            Con.Close();
        }
        private void populateprod(string cat)
        {
            Con.Open();
            string query = "";
            if(cat == "")
            {
                query = "Select ProdName,ProdPriceSell,ProdQty from Prod";
            }
            else
            {
                query = "Select ProdName,ProdPriceSell,ProdQty from Prod where ProdCat = '" + cat + "'";
            }

            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            selproddgv.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void populatebill()
        {
            Con.Open();
            string query = "select * from Bill";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            billdgv.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void fillproddgv()
        {
            DataTable table = new DataTable();
            table.Columns.Add("#".ToString());
            table.Columns.Add("Product".ToString());
            table.Columns.Add("Price".ToString());
            table.Columns.Add("Qty".ToString());
            table.Columns.Add("Total".ToString());

            orderdgv.DataSource = table;
        }

        public DataTable GetResultsTable(DataTable table)
        {
            DataRow dr = table.NewRow();
            dr["#"] = n + 1;
            dr["Product"] = billprod.Text;
            dr["Price"] = billprice.Text;
            dr["Qty"] = billqty.Text;
            dr["Total"] = int.Parse(billprice.Text) * int.Parse(billqty.Text);
            table.Rows.Add(dr);
            return table;
        }

        private void Selling_Load(object sender, EventArgs e)
        {
            fillcat();
            fillpay();
            fillproddgv();
            populateprod("");
            populatebill();

            if (this.user != "Admin")
            {
                button1.Hide();
                button2.Hide();
                button3.Hide();
                button9.Hide();
                button10.Hide();
            }
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            populateprod(selprodcat.Text);
        }

        int GrandTotal = 0, n = 0;

        private void button6_Click(object sender, EventArgs e)
        {
            
            if(billid.Text == "")
            {
                
                MessageBox.Show("Missing Bill ID!");
            }
            else
            {
                int x = orderdgv.Rows.Count;
                try
                {
                    
                    foreach (DataGridViewRow row in orderdgv.Rows)
                    {
                        
                        if (--x != 0)
                        {
                            string product_name = row.Cells[1].Value.ToString();
                            string product_qty = row.Cells[3].Value.ToString();
                            Con.Open();
                            string queryselect = "select * from Prod WHERE ProdName = '" + product_name + "'";
                            SqlCommand cmd1 = new SqlCommand(queryselect, Con);
                            SqlDataReader hasil = cmd1.ExecuteReader();

                            if (!hasil.HasRows)
                            {
                                string msg = "Product " + product_name + "does not exist!";
                                MessageBox.Show(msg);
                            }
                            else
                            {
                                hasil.Read();
                                string qty = hasil["ProdQty"].ToString();
                                string queryupdate = "update Prod set ProdQty = " + (int.Parse(qty) - int.Parse(product_qty)) + ",ProdSold = " + product_qty + " where ProdName = '" + product_name + "'";
                                Con.Close();
                                Con.Open();
                                SqlCommand cmd2 = new SqlCommand(queryupdate, Con);
                                cmd2.ExecuteNonQuery();
                                Con.Close();
                            }
                        }
                    }
                    Con.Open();
                    string query = "insert into Bill values(" + billid.Text + ",'" + this.user + "'," + GrandTotal + ",'" + selpay.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Added Successfully");
                    Con.Close();
                    populatebill();
                    fillproddgv();
                    populateprod("");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + x);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Report rep =  new Report();
            rep.Show();
            this.Hide();
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

        private void button3_Click(object sender, EventArgs e)
        {
            Product prod = new Product();
            prod.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoginForm log = new LoginForm();
            log.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (billprod.Text == "" || billprice.Text == "" || billqty.Text == "")
            {
                MessageBox.Show("Missing Product Data");
            }
            else
            {
                orderdgv.DataSource = GetResultsTable((DataTable)orderdgv.DataSource); 
                n++;
                GrandTotal = GrandTotal + int.Parse(billprice.Text) * int.Parse(billqty.Text);
                amountidr.Text = "IDR " + GrandTotal;
            }
        }
       
    }
}
