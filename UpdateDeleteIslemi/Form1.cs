using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace UpdateDeleteIslemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(Tools.ConnectionString);
      

        public void KategoriDoldur()
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();
            SqlCommand cmd = new SqlCommand("SELECT CategoryId, CategoryName FROM Categories ", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
           
            if (dr.HasRows)
                comboBox1.Items.Clear();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["CategoryName"].ToString());
            }
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            KategoriDoldur();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Categories WHERE CategoryName=@catName", baglanti);
                cmd.Parameters.AddWithValue("@catName", comboBox1.SelectedItem.ToString());
               


                int donenDeger = cmd.ExecuteNonQuery();
                KategoriDoldur();
                MessageBox.Show(donenDeger != 0 ? "ISLEM BASARILI" : "ISLEM BASARISIZ");
                baglanti.Close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE Categories SET CategoryName=@catName WHERE CategoryName=@Name", baglanti);
                cmd.Parameters.AddWithValue("@catName", textBox1.Text);
                cmd.Parameters.AddWithValue("@Name", comboBox1.SelectedItem.ToString());


                int donenDeger = cmd.ExecuteNonQuery();
                    KategoriDoldur();
                    MessageBox.Show(donenDeger != 0 ? "ISLEM BASARILI" : "ISLEM BASARISIZ");
            baglanti.Close();

                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Categories (CategoryName)" +
                        " VALUES(@catName) ", baglanti);

                    cmd.Parameters.AddWithValue("@catName", textBox1.Text);
                   

                    int donenDeger = cmd.ExecuteNonQuery();
                    KategoriDoldur();
                    MessageBox.Show(donenDeger != 0 ? "ISLEM BASARILI" : "ISLEM BASARISIZ");
                    baglanti.Close();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.SelectedItem.ToString();
        }
    }
}
