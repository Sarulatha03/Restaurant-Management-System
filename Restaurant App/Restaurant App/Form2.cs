using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restaurant_App
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestaurantDB;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            string Foodtype = "";
            if (radioButton1.Checked == true)
            {
                Foodtype = "Veg";
            }
            else
            {
                Foodtype = "Non Veg";
            }
            try { 
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into TblFood(Fname,Ftype,Fprice,Favailable) values('" + textBox1.Text + "','" + Foodtype +"'," + textBox2.Text + ",'" + comboBox1.Text + "')", con);
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    MessageBox.Show("Submitted Successfully...");
                }
                else
                {
                    MessageBox.Show("Not Submitted!");
                }
            }
            //con.Close();
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                    con.Close();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from TblFood where Fname='" + textBox1.Text + "'", con);
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
            {
                MessageBox.Show("Deleted Submitted...");
            }
            else
            {
                MessageBox.Show("Not Deleted!");
            }
            con.Close();
        }
    

    private void button3_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("select * from TblFood", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update TblFood set Fprice=" + textBox2.Text + " where Fname = '" + textBox1.Text + "'", con);
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
            {
                MessageBox.Show("Updated Submitted...");
            }
            else
            {
                MessageBox.Show("Not Updated!");
            }
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from TblFood where Fname = '" + dataGridView1.CurrentCell.Value + "' ", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
               
                textBox1.Text = sdr["Fname"].ToString();
                textBox2.Text = sdr["Fprice"].ToString();
                comboBox1.Text = sdr["Favailable"].ToString();
                if (sdr["Ftype"].ToString() == "Veg")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
            }
            con.Close();
        }
    }
}
