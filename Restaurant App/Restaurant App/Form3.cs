using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Restaurant_App
{
    public partial class Form3 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestaurantDB;Integrated Security=True");
        public Form3()
        {
            InitializeComponent();
            LoadFoodNames();
        }
        private void LoadFoodNames()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT FName FROM TblFood ORDER BY FName ASC", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "FName";
            //comboBox1.ValueMember = "FName";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT FPrice FROM TblFood WHERE FName = '" + comboBox1.Text + "' ";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            object price = cmd.ExecuteScalar();
            con.Close();

            if (price != null)
            {
                textBox2.Text = price.ToString();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox3.Text, out int quantity) && int.TryParse(textBox2.Text, out int price))
            {
                textBox4.Text = (quantity * price).ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();

            string query1 = "SELECT Fid FROM TblFood WHERE FName = '" + comboBox1.Text + "' ";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            object fid = cmd1.ExecuteScalar();

            string query2 = "INSERT INTO TblBilling(Fid, Fname, Price, Quantity, Amount) VALUES("+ fid + ", '"
                + comboBox1.Text + "', '"+ textBox2.Text + "', "+ textBox3.Text + ", '"+ textBox4.Text + "')";

            SqlCommand cmd = new SqlCommand(query2, con);

            int r = cmd.ExecuteNonQuery();

            if (r > 0)
            {
                MessageBox.Show("Added Successfully...");
            }
            else
            {
                MessageBox.Show("Not Added!");
            }

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("select * from TblBilling", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            //Total
            SqlCommand cmd = new SqlCommand("SELECT SUM(Amount) FROM TblBilling", con);
            con.Open();
            object totalAmount = cmd.ExecuteScalar();
            con.Close();
            if (totalAmount != null)
            {
                textBox5.Text = totalAmount.ToString();
            }
        }       
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //not use
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //not use
        }
    }
}
