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
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\pos.mdf;Integrated Security=True;Connect Timeout=30");

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void fillcombo()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select PaymentName from Payment", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PaymentName", typeof(string));
            dt.Load(rdr);
            comboBox2.ValueMember = "PaymentName";
            comboBox2.DataSource = dt;

            Con.Close();
        }
        public DataTable GetResultsTable()
        {
            
            DataTable table = new DataTable();
            table.Columns.Add("Seller".ToString());
            table.Columns.Add("Transactions".ToString());
            return table;
        }

        private void populateSellerReport()
        {
            Con.Open();
            string query = "select BillSeller as 'Seller Name' , count(BillSeller) as Total from Bill";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();
           
        }
        private void populateSellsList()
        {
            Con.Open();
            string query = "select * from Bill";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void populateMostBuyingProducts()
        {
            Con.Open();
            string query = "select ProdName as 'Product Name', ProdJual as 'Total' from Prod";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            Con.Close();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Report_Load(object sender, EventArgs e)
        {
            fillcombo();
            populateSellsList();
            populateSellerReport();
            populateMostBuyingProducts();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "select * from Bill where BillPayment = "+ comboBox2.Text;
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            Con.Close();
        }
    }
}
