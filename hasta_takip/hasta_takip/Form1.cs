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

namespace hasta_takip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection sqlCon = new SqlConnection("Server=.; Database=databaseName;Integrated Security=True;");

        private void button1_Click(object sender, EventArgs e)
        {
            sqlCon.Open();

            SqlCommand cmd = new SqlCommand("select * from kullanici", sqlCon);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if(textBox1.Text == rdr["kullaniciAdi"].ToString() && textBox2.Text == rdr["sifre"].ToString())
                {
                    Form2 anasayfa = new Form2();
                    anasayfa.Show();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya Şifreyi Yanlış Girdiniz.");
                }
            }

            sqlCon.Close();
        }
    }
}
