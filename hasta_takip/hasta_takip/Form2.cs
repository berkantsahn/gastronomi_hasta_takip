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

namespace hasta_takip
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string tc, ad, soyad, anaAdi, babaAdi, cadde, sokak, evtel, ceptel, digertel, alkolAck, sigaraAck, email, not;
        public int dogumYeriIl, dogumYeriIlce, aptNo, yasadigiIl, yasadigiIlce, cinsiyet, kanGrubu, medeniDurum, sosyal, alkol, sigara, katNo, daireNo;
        DateTime dogumTarihi;
        //veritabanı bağlantısı gerçekleştirildi.
        SqlConnection sqlCon = new SqlConnection("Server=.; Database=gastroOtomasyon;Integrated Security=True;");
        SqlDataAdapter sqlAdapter = new SqlDataAdapter();
        DataSet sqlDataSet = new DataSet();


        //hastaBilgileri hBilgileri = new hastaBilgileri();

        private void textBoxClear()
        {
            //Hasta ekle kısmındaki textboxlar temizlendi.
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox18.Clear();
            textBox61.Clear();
            textBox62.Clear();
            comboBox2.Text = "";
            comboBox6.Text = "";
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null;
            comboBox4.SelectedItem = null;
            comboBox5.SelectedItem = null;
            comboBox6.SelectedItem = null;
            comboBox2.Enabled = false;
            comboBox6.Enabled = false;


        }
        //Veri çekme başlangıç
        private void gridViewDoldur()
        {
            sqlAdapter = new SqlDataAdapter("select hb.kayitNo as 'Kayıt No', hb.tc as 'TC', hb.hAdi as 'Ad', hb.hSoyadi as 'Soyad', hb.hAnneAdi as 'Anne Adı', hb.hBabaAdi as 'Baba Adı', il1.sehir as 'Doğum Yeri İl', ilce1.ilce as 'Doğum Yeri İlçe', hDogumTarihi as 'Doğum Tarihi', cins.cinsiyetAd as 'Cinsiyet', kg.kanAd as 'Kan Grubu', il2.sehir as 'Adres İl', ilce2.ilce as 'Adres İlçe', hAdresCadde as 'Cadde', hAdresSokak as 'Sokak', hAptNo as 'Apartman No', hAdresKatNo as 'Kat No', hAdresDaireNo as 'Daire No', hEvTel as 'Ev Telefonu', hCeptel as 'Cep Telefonu', hDigerTel as 'Diğer Telefon', hMail as 'E-Posta', md.medeniAd as 'Medeni Durum', sgk.sgkAd as 'SGK', al.durumAd as 'Alkol Durumu', hAlkolAck as 'Alkol Açıklama', sg.durumAd as 'Sigara Durumu', hSigaraAck as 'Sigara Açıklama', hNot as 'Hasta Not' from hastaBilgileri as hb INNER JOIN cinsiyetler as cins on hb.hCinsiyetId = cins.cinsiyetId INNER JOIN medeniDurum as md on hb.hMedeni = md.medeniId INNER JOIN sgkDurum as sgk on hb.hSosyal = sgk.sgkId INNER JOIN kanGruplari as kg on hb.hKanGrubuId = kg.kanId INNER JOIN alSgDurumlari as sg on hb.hSigaraId = sg.id INNER JOIN alSgDurumlari as al on hb.hAlkolId = al.id INNER JOIN ilceler as ilce2 on hb.hAdresIlceId = ilce2.id INNER JOIN ilceler as ilce1 on hb.hDogumIlceId = ilce1.id INNER JOIN iller as il1 on hb.hDogumIlId = il1.id INNER JOIN iller as il2 on hb.hAdresIlId = il2.id", sqlCon);
            sqlDataSet = new DataSet();
            sqlCon.Open();
            sqlAdapter.Fill(sqlDataSet, "hastaBilgileri");
            dataGridView1.DataSource = sqlDataSet.Tables[0];
            sqlCon.Close();
        }
        //Veri çekme son



        private void comboCek()
        {
            //İller çekiliyor. -> Ekle
            comboBox2.Enabled = false;
            comboBox6.Enabled = false;

            sqlCon.Open();
            SqlCommand sqlCommandIl = new SqlCommand("select sehir from iller", sqlCon);
            SqlDataReader sqlReaderIl = sqlCommandIl.ExecuteReader();
            while (sqlReaderIl.Read())
            {
                comboBox1.Items.Add(sqlReaderIl["sehir"].ToString());
                comboBox5.Items.Add(sqlReaderIl["sehir"].ToString());
                comboBox20.Items.Add(sqlReaderIl["sehir"].ToString());
                comboBox14.Items.Add(sqlReaderIl["sehir"].ToString());

            }

            sqlCon.Close();

            sqlCon.Open();
            SqlCommand sqlCommandKan = new SqlCommand("select kanAd from kanGruplari", sqlCon);
            SqlDataReader sqlReaderKan = sqlCommandKan.ExecuteReader();
            while (sqlReaderKan.Read())
            {
                comboBox3.Items.Add(sqlReaderKan["kanAd"].ToString());
                comboBox18.Items.Add(sqlReaderKan["kanAd"].ToString());
            }
            sqlCon.Close();
            sqlCon.Open();
            SqlCommand sqlCommandSosyal = new SqlCommand("select sgkAd from sgkDurum", sqlCon);
            SqlDataReader sqlReaderSosyal = sqlCommandSosyal.ExecuteReader();
            while (sqlReaderSosyal.Read())
            {
                comboBox4.Items.Add(sqlReaderSosyal["sgkAd"].ToString());
                comboBox17.Items.Add(sqlReaderSosyal["sgkAd"].ToString());
            }
            sqlCon.Close();

        }

        private void ilacCek()
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("select * from ilaclar", sqlCon);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                listBox1.Items.Add(rdr.GetString(1));
            }

            sqlCon.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ilacCek();
            //Muayene işlemleri kısmında bilgi değişikliği olmaması için veriler kapatıldı.
            textBox35.Enabled = false;
            textBox36.Enabled = false;
            textBox37.Enabled = false;
            textBox38.Enabled = false;
            textBox39.Enabled = false;
            textBox65.Enabled = false;
            textBox66.Enabled = false;
            dateTimePicker4.Enabled = false;
            radioButton23.Enabled = false;
            radioButton24.Enabled = false;
            radioButton17.Enabled = false;
            radioButton18.Enabled = false;
            comboBox16.Enabled = false;
            comboBox13.Enabled = false;



            radioButton19.Enabled = false;
            radioButton20.Enabled = false;
            radioButton21.Enabled = false;
            radioButton22.Enabled = false;
            radioButton29.Enabled = false;
            radioButton30.Enabled = false;
            dateTimePicker3.Enabled = false;


            radioButton2.Checked = true;
            radioButton3.Checked = true;
            radioButton5.Checked = true;
            radioButton7.Checked = true;

            //KayıtNo kısmına değer girilmesi engellendi.
            textBox2.Enabled = false;
            textBox2.Text = "Otomatik Atanacaktır";

            //Güncelleme sayfasıda tc, kayıt no değişemez.
            textBox83.Enabled = false;
            textBox84.Enabled = false;


            comboCek();
            //Textboxların içerisine girilebilecek maximum karakter sayısı belirlendi.
            textBox3.MaxLength = 11;
            textBox13.MaxLength = 11;
            textBox14.MaxLength = 11;
            textBox15.MaxLength = 11;
            textBox69.MaxLength = 11;
            textBox70.MaxLength = 11;
            textBox71.MaxLength = 11;

            //datagridview sadece okunabilir yapıldı.
            dataGridView1.ReadOnly = true;

            gridViewDoldur();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox51_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void label90_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            int cinsiyet = 0, kanGrubu = 0, sosyal = 0, sigara = 0, alkol = 0, medeni = 0;
            if (radioButton39.Checked)
            {
                cinsiyet = 1;
            }
            else if (radioButton40.Checked)
            {
                cinsiyet = 2;
            }

            if (radioButton36.Checked)
            {
                alkol = 1;
            }
            else if (radioButton35.Checked)
            {
                alkol = 2;
            }
            else if (radioButton34.Checked)
            {
                alkol = 3;
            }

            if (radioButton33.Checked)
            {
                sigara = 1;
            }
            else if (radioButton32.Checked)
            {
                sigara = 2;
            }
            else if (radioButton31.Checked)
            {
                sigara = 3;
            }
            if (radioButton37.Checked)
            {
                medeni = 2;
            }
            else if (radioButton38.Checked)
            {
                medeni = 1;
            }

            sqlCon.Open();
            SqlCommand cmdGuncelle = new SqlCommand("UPDATE hastaBilgileri set hAdi='" + textBox82.Text + "', hSoyadi='" + textBox81.Text + "',hAnneAdi='" + textBox80.Text + "',hBabaAdi='" + textBox79.Text + "',hAdresCadde='" + textBox76.Text + "',hAdresSokak='" + textBox75.Text + "',hAptNo='" + textBox74.Text + "',hAdresKatNo='" + textBox73.Text + "',hAdresDaireNo='" + textBox72.Text + "',hEvTel='" + textBox71.Text + "',hCepTel='" + textBox70.Text + "',hDigerTel='" + textBox69.Text + "',hMail='" + textBox68.Text + "',hNot='" + textBox67.Text + "',hAlkolAck='" + textBox78.Text + "',hSigaraAck='" + textBox77.Text + "',hSosyal='" + (comboBox17.SelectedIndex + 1) + "',hKanGrubuId='" + (comboBox18.SelectedIndex + 1) + "',hDogumIlId='" + (comboBox20.SelectedIndex + 1) + "',hAdresIlId='" + (comboBox14.SelectedIndex + 1) + "' where kayitNo='" + int.Parse(textBox84.Text) + "' ", sqlCon);
            if (cmdGuncelle.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Kayıt güncellendi");
            }
            else
            {
                MessageBox.Show("Hata oluştu.");
            }
            sqlCon.Close();
        }

        private void label87_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //HastaBilgileri kısmında tc'ye göre ara butonu kısmı oluşturuldu.
            sqlAdapter = new SqlDataAdapter("select hb.kayitNo as 'Kayıt No', hb.tc as 'TC', hb.hAdi as 'Ad', hb.hSoyadi as 'Soyad', hb.hAnneAdi as 'Anne Adı', hb.hBabaAdi as 'Baba Adı', il1.sehir as 'Doğum Yeri İl', ilce1.ilce as 'Doğum Yeri İlçe', hDogumTarihi as 'Doğum Tarihi', cins.cinsiyetAd as 'Cinsiyet', kg.kanAd as 'Kan Grubu', il2.sehir as 'Adres İl', ilce2.ilce as 'Adres İlçe', hAdresCadde as 'Cadde', hAdresSokak as 'Sokak', hAptNo as 'Apartman No', hAdresKatNo as 'Kat No', hAdresDaireNo as 'Daire No', hEvTel as 'Ev Telefonu', hCeptel as 'Cep Telefonu', hDigerTel as 'Diğer Telefon', hMail as 'E-Posta', md.medeniAd as 'Medeni Durum', sgk.sgkAd as 'SGK', al.durumAd as 'Alkol Durumu', hAlkolAck as 'Alkol Açıklama', sg.durumAd as 'Sigara Durumu', hSigaraAck as 'Sigara Açıklama', hNot as 'Hasta Not' from hastaBilgileri as hb INNER JOIN cinsiyetler as cins on hb.hCinsiyetId = cins.cinsiyetId INNER JOIN medeniDurum as md on hb.hMedeni = md.medeniId INNER JOIN sgkDurum as sgk on hb.hSosyal = sgk.sgkId INNER JOIN kanGruplari as kg on hb.hKanGrubuId = kg.kanId INNER JOIN alSgDurumlari as sg on hb.hSigaraId = sg.id INNER JOIN alSgDurumlari as al on hb.hAlkolId = al.id INNER JOIN ilceler as ilce2 on hb.hAdresIlceId = ilce2.id INNER JOIN ilceler as ilce1 on hb.hDogumIlceId = ilce1.id INNER JOIN iller as il1 on hb.hDogumIlId = il1.id INNER JOIN iller as il2 on hb.hAdresIlId = il2.id where hb.tc=" + textBox1.Text, sqlCon);
            sqlDataSet = new DataSet();
            sqlCon.Open();
            sqlAdapter.Fill(sqlDataSet, "hastaBilgileri");
            dataGridView1.DataSource = sqlDataSet.Tables[0];
            sqlCon.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //HastaBilgileri yenile butonu.
            textBox1.Text = "";
            gridViewDoldur();
            MessageBox.Show("Kayıtlar Yenilendi!");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //illere göre ilçelere atama yapıldı.  Doğum il-ilçe
            comboBox2.Enabled = true;
            comboBox2.Items.Clear();
            sqlCon.Open();
            SqlCommand sqlCommandIlce = new SqlCommand("select * from ilceler where sehir=" + (comboBox1.SelectedIndex + 1), sqlCon);
            SqlDataReader sqlReaderIlce = sqlCommandIlce.ExecuteReader();
            while (sqlReaderIlce.Read())
            {
                comboBox2.Items.Add(sqlReaderIlce["ilce"].ToString());

            }

            sqlCon.Close();

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //illere göre ilçelere atama yapıldı. Adres il-ilçe
            comboBox6.Enabled = true;
            comboBox6.Items.Clear();
            sqlCon.Open();
            SqlCommand sqlCommandIlce = new SqlCommand("select * from ilceler where sehir=" + (comboBox5.SelectedIndex + 1), sqlCon);
            SqlDataReader sqlReaderIlce = sqlCommandIlce.ExecuteReader();
            while (sqlReaderIlce.Read())
            {
                comboBox6.Items.Add(sqlReaderIlce["ilce"].ToString());

            }

            sqlCon.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int secimDogum = -1;
            int secimAdres = -1;

            SqlCommand sqlIlceSecimDogum = new SqlCommand("select id from ilceler where ilce='" + comboBox2.SelectedItem.ToString() + "'", sqlCon);
            sqlCon.Open();
            SqlDataReader rdr = sqlIlceSecimDogum.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                secimDogum = Convert.ToInt32(rdr.GetValue(0));

            }
            sqlCon.Close();

            SqlCommand sqlIlceSecimAdres = new SqlCommand("select id from ilceler where ilce='" + comboBox6.SelectedItem.ToString() + "'", sqlCon);
            sqlCon.Open();
            SqlDataReader rdr1 = sqlIlceSecimAdres.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr1.Read())
            {

                secimAdres = Convert.ToInt32(rdr1.GetValue(0));
            }
            sqlCon.Close();

            if (textBox3.TextLength != 11 || textBox14.TextLength != 11)
            {
                MessageBox.Show("TC Kimlik Numarası ve Cep Telefonu Numarası 11 Haneden Küçük Olamaz.");
            }
            else
            {

                if (textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" || textBox11.Text == "" || textBox12.Text == "" || textBox14.Text == "" || textBox16.Text == "")
                {
                    MessageBox.Show("Boş Alan Bırakılamaz.");
                }
                else
                {

                    if (radioButton1.Checked)
                    {
                        cinsiyet = 2;
                    }
                    else if (radioButton2.Checked)
                    {
                        cinsiyet = 1;
                    }

                    if (radioButton3.Checked)
                    {
                        medeniDurum = 2;
                    }
                    else if (radioButton4.Checked)
                    {
                        medeniDurum = 1;
                    }

                    if (radioButton6.Checked)
                    {
                        alkol = 1;
                    }
                    else if (radioButton5.Checked)
                    {
                        alkol = 2;
                    }
                    else if (radioButton25.Checked)
                    {
                        alkol = 3;
                    }

                    if (radioButton8.Checked)
                    {
                        sigara = 1;
                    }
                    else if (radioButton7.Checked)
                    {
                        sigara = 2;
                    }
                    else if (radioButton26.Checked)
                    {
                        sigara = 3;
                    }

                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                        string kayit = "insert into hastaBilgileri(tc, hAdi, hSoyadi, hAnneAdi, hBabaAdi, hDogumIlId, hDogumIlceId, hDogumTarihi, hCinsiyetId, hKanGrubuId, hAdresIlId, hAdresIlceId, hAdresCadde, hAdresSokak, hAptNo, hAdresKatNo, hAdresDaireNo, hEvtel, hCeptel, hDigerTel, hMail, hMedeni, hSosyal, hAlkolId, hAlkolAck, hSigaraId, hSigaraAck, hNot) values (@tcno,@ad,@soyad,@anneAdi,@babaAdi,@dogumYerIl,@dogumYeriIlce,@dogumTarihi,@cinsiyet,@kanGrubu,@yasadigiIl,@yasadigiIlce,@cadde,@sokak,@aptNo,@katNo,@daireNo,@evTel,@cepTel,@digerTel,@email,@medeni,@sosyal,@alkol,@alkolAck,@sigara,@sigaraAck,@not)";
                        // hastaBilgileri tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                        SqlCommand komut = new SqlCommand(kayit, sqlCon);
                        //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                        komut.Parameters.AddWithValue("@tcno", textBox3.Text);
                        komut.Parameters.AddWithValue("@ad", textBox4.Text);
                        komut.Parameters.AddWithValue("@soyad", textBox5.Text);
                        komut.Parameters.AddWithValue("@anneadi", textBox6.Text);
                        komut.Parameters.AddWithValue("@babaadi", textBox7.Text);
                        komut.Parameters.AddWithValue("@dogumYerIl", comboBox1.SelectedIndex + 1);
                        komut.Parameters.AddWithValue("@dogumYeriIlce", secimDogum);
                        komut.Parameters.AddWithValue("@dogumTarihi", dateTimePicker1.Value);
                        komut.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                        komut.Parameters.AddWithValue("@kanGrubu", comboBox3.SelectedIndex + 1);
                        komut.Parameters.AddWithValue("@yasadigiIl", comboBox5.SelectedIndex + 1);
                        komut.Parameters.AddWithValue("@yasadigiIlce", secimAdres);
                        komut.Parameters.AddWithValue("@cadde", textBox8.Text);
                        komut.Parameters.AddWithValue("@sokak", textBox9.Text);
                        komut.Parameters.AddWithValue("@aptNo", int.Parse(textBox10.Text));
                        komut.Parameters.AddWithValue("@katNo", int.Parse(textBox11.Text));
                        komut.Parameters.AddWithValue("@daireNo", int.Parse(textBox12.Text));
                        komut.Parameters.AddWithValue("@evTel", textBox13.Text);
                        komut.Parameters.AddWithValue("@cepTel", textBox14.Text);
                        komut.Parameters.AddWithValue("@digerTel", textBox15.Text);
                        komut.Parameters.AddWithValue("@email", textBox16.Text);
                        komut.Parameters.AddWithValue("@medeni", medeniDurum);
                        komut.Parameters.AddWithValue("@sosyal", comboBox4.SelectedIndex + 1);
                        komut.Parameters.AddWithValue("@alkol", alkol);
                        komut.Parameters.AddWithValue("@alkolAck", textBox61.Text);
                        komut.Parameters.AddWithValue("@sigara", sigara);
                        komut.Parameters.AddWithValue("@sigaraAck", textBox62.Text);
                        komut.Parameters.AddWithValue("@not", textBox18.Text);



                        //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                        komut.ExecuteNonQuery();
                        //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                        sqlCon.Close();
                        MessageBox.Show("Hasta Kayıt İşlemi Gerçekleşti.");
                        textBoxClear();
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
                    }



                }
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            //İptal butonu anasayfaya yönlendiriyor.
            tabPage1.Show();
            textBoxClear();
        }

        private void button15_Click(object sender, EventArgs e)
        {


            MessageBox.Show(comboBox2.SelectedItem.ToString());
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }


        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {


        }

        private void textBox33_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox24_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox25_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox26_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox32_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar);
        }

        private void textBox31_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
        }

        private void textBox30_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void textBox29_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void textBox82_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox81_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox80_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox79_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox74_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox73_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox72_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox71_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox70_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox69_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            DialogResult dRes = new DialogResult();
            dRes = MessageBox.Show("Güncelleme sayfasına gitmek istiyorsanız EVET'i Muayene Sayfasına Gitmek İstiyorsanız HAYIR'ı tıklayınız.", "SORU!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);


            if (dRes == DialogResult.Yes)
            {

                //seçili olan hücrede tc almak.
                string current;
                current = dataGridView1.CurrentRow.Cells["TC"].Value.ToString();

                SqlCommand sqlCmdGuncelle = new SqlCommand("select * from hastaBilgileri where tc='" + current + "'", sqlCon);
                int kayitNo = 0;
                sqlCon.Open();
                SqlDataReader rdr = sqlCmdGuncelle.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {



                    kayitNo = Convert.ToInt32(rdr.GetValue(0));
                    tc = rdr.GetString(1);
                    ad = rdr.GetString(2);
                    soyad = rdr.GetString(3);
                    anaAdi = rdr.GetString(4);
                    babaAdi = rdr.GetString(5);
                    dogumYeriIl = Convert.ToInt32(rdr.GetValue(6));
                    dogumYeriIlce = Convert.ToInt32(rdr.GetValue(7));
                    dogumTarihi = Convert.ToDateTime(rdr.GetValue(8));
                    cinsiyet = Convert.ToInt32(rdr.GetValue(9));
                    kanGrubu = Convert.ToInt32(rdr.GetValue(10));
                    yasadigiIl = Convert.ToInt32(rdr.GetValue(11));
                    yasadigiIlce = Convert.ToInt32(rdr.GetValue(12));
                    cadde = rdr.GetString(13);
                    sokak = rdr.GetString(14);
                    aptNo = Convert.ToInt32(rdr.GetValue(15));
                    katNo = Convert.ToInt32(rdr.GetValue(16));
                    daireNo = Convert.ToInt32(rdr.GetValue(17));
                    evtel = (rdr.GetString(18));
                    ceptel = (rdr.GetString(19));
                    digertel = (rdr.GetString(20));
                    email = (rdr.GetString(21));
                    medeniDurum = Convert.ToInt32(rdr.GetValue(22));
                    sosyal = Convert.ToInt32(rdr.GetValue(23));
                    alkol = Convert.ToInt32(rdr.GetValue(24));
                    alkolAck = rdr.GetString(25);
                    sigara = Convert.ToInt32(rdr.GetValue(26));
                    sigaraAck = rdr.GetString(27);
                    not = rdr.GetString(28);

                }

                textBox84.Text = kayitNo.ToString();
                textBox83.Text = tc;
                textBox82.Text = ad;
                textBox81.Text = soyad;
                textBox80.Text = anaAdi;
                textBox79.Text = babaAdi;
                comboBox20.Text = dogumYeriIl.ToString();
                comboBox19.Text = dogumYeriIlce.ToString();
                dateTimePicker4.Text = dogumTarihi.ToString();
                textBox76.Text = cadde;
                textBox75.Text = sokak;
                textBox74.Text = aptNo.ToString();
                textBox73.Text = daireNo.ToString();
                textBox72.Text = katNo.ToString();
                textBox71.Text = evtel;
                textBox70.Text = ceptel;
                textBox69.Text = digertel;
                textBox68.Text = email;
                textBox67.Text = not;
                textBox78.Text = alkolAck;
                textBox77.Text = sigaraAck;
                if (cinsiyet == 1)
                {
                    radioButton40.Checked = false;
                    radioButton39.Checked = true;
                }
                else if (cinsiyet == 2)
                {
                    radioButton40.Checked = true;
                    radioButton39.Checked = false;
                }


                if (medeniDurum == 1)
                {
                    radioButton38.Checked = false;
                    radioButton37.Checked = true;
                }
                else if (medeniDurum == 2)
                {
                    radioButton38.Checked = true;
                    radioButton37.Checked = false;
                }

                if (alkol == 1)
                {
                    radioButton36.Checked = true;
                    radioButton35.Checked = false;
                    radioButton34.Checked = false;
                }
                else if (alkol == 2)
                {
                    radioButton36.Checked = false;
                    radioButton35.Checked = true;
                    radioButton34.Checked = false;
                }
                else if (alkol == 3)
                {
                    radioButton36.Checked = false;
                    radioButton35.Checked = false;
                    radioButton34.Checked = true;
                }

                if (sigara == 1)
                {
                    radioButton33.Checked = true;
                    radioButton32.Checked = false;
                    radioButton31.Checked = false;
                }
                else if (sigara == 2)
                {
                    radioButton33.Checked = false;
                    radioButton32.Checked = true;
                    radioButton31.Checked = false;
                }
                else if (sigara == 3)
                {
                    radioButton33.Checked = false;
                    radioButton32.Checked = false;
                    radioButton31.Checked = true;
                }

                //güncelleme sayfası il ilçe çekme kısmı.

                sqlCon.Close();
                SqlCommand dogumYeriIlceCek = new SqlCommand("select * from ilceler where id=" + dogumYeriIlce, sqlCon);
                sqlCon.Open();
                SqlDataReader dgmIlcRdr = dogumYeriIlceCek.ExecuteReader(CommandBehavior.CloseConnection);
                while (dgmIlcRdr.Read())
                {

                    comboBox19.Text = dgmIlcRdr.GetString(1);

                }

                sqlCon.Close();

                SqlCommand dogumYeriIlCek = new SqlCommand("select * from iller where id=" + dogumYeriIl, sqlCon);
                sqlCon.Open();
                SqlDataReader dgmIlRdr = dogumYeriIlCek.ExecuteReader(CommandBehavior.CloseConnection);

                while (dgmIlRdr.Read())
                {
                    comboBox20.Text = dgmIlRdr.GetString(1);
                }

                sqlCon.Close();

                SqlCommand adresYeriIlceCek = new SqlCommand("select * from ilceler where id=" + yasadigiIlce, sqlCon);
                sqlCon.Open();
                SqlDataReader adresIlcRdr = adresYeriIlceCek.ExecuteReader(CommandBehavior.CloseConnection);
                while (adresIlcRdr.Read())
                {

                    comboBox15.Text = adresIlcRdr.GetString(1);

                }

                sqlCon.Close();
                SqlCommand adresYeriIlCek = new SqlCommand("select * from iller where id=" + yasadigiIl, sqlCon);
                sqlCon.Open();
                SqlDataReader adresIlRdr = adresYeriIlCek.ExecuteReader(CommandBehavior.CloseConnection);

                while (adresIlRdr.Read())
                {
                    comboBox14.Text = adresIlRdr.GetString(1);
                }

                sqlCon.Close();

                SqlCommand kanGrubuCek = new SqlCommand("select * from kanGruplari where kanId=" + kanGrubu, sqlCon);
                sqlCon.Open();
                SqlDataReader kanRdr = kanGrubuCek.ExecuteReader(CommandBehavior.CloseConnection);
                while (kanRdr.Read())
                {

                    comboBox18.Text = kanRdr.GetString(1);

                }

                sqlCon.Close();

                SqlCommand sosyalCek = new SqlCommand("select * from sgkDurum where sgkId=" + sosyal, sqlCon);
                sqlCon.Open();
                SqlDataReader sosyalRdr = sosyalCek.ExecuteReader(CommandBehavior.CloseConnection);
                while (sosyalRdr.Read())
                {

                    comboBox17.Text = sosyalRdr.GetString(1);

                }
                sqlCon.Close();

                tabControl1.SelectedTab = tabPage3;
            }
            else if (dRes == DialogResult.No)
            {
                string current;
                current = dataGridView1.CurrentRow.Cells["TC"].Value.ToString();

                SqlCommand sqlCmdGuncelle = new SqlCommand("select * from hastaBilgileri where tc='" + current + "'", sqlCon);
                int kayitNo = 0;
                sqlCon.Open();
                SqlDataReader rdr = sqlCmdGuncelle.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {



                    kayitNo = Convert.ToInt32(rdr.GetValue(0));
                    tc = rdr.GetString(1);
                    ad = rdr.GetString(2);
                    soyad = rdr.GetString(3);
                    dogumTarihi = Convert.ToDateTime(rdr.GetValue(8));
                    cinsiyet = Convert.ToInt32(rdr.GetValue(9));
                    kanGrubu = Convert.ToInt32(rdr.GetValue(10));
                    ceptel = (rdr.GetString(19));
                    medeniDurum = Convert.ToInt32(rdr.GetValue(22));
                    sosyal = Convert.ToInt32(rdr.GetValue(23));
                    alkol = Convert.ToInt32(rdr.GetValue(24));
                    alkolAck = rdr.GetString(25);
                    sigara = Convert.ToInt32(rdr.GetValue(26));
                    sigaraAck = rdr.GetString(27);


                }
                textBox35.Text = kayitNo.ToString();
                textBox36.Text = tc;
                textBox37.Text = ad;
                textBox38.Text = soyad;
                dateTimePicker3.Text = dogumTarihi.ToString();
                textBox39.Text = ceptel;
                textBox66.Text = alkolAck;
                textBox65.Text = sigaraAck;
                if (cinsiyet == 1)
                {
                    radioButton24.Checked = false;
                    radioButton23.Checked = true;
                }
                else if (cinsiyet == 2)
                {
                    radioButton24.Checked = true;
                    radioButton23.Checked = false;
                }


                if (medeniDurum == 1)
                {
                    radioButton18.Checked = false;
                    radioButton17.Checked = true;
                }
                else if (medeniDurum == 2)
                {
                    radioButton18.Checked = true;
                    radioButton17.Checked = false;
                }

                if (sigara == 1)
                {
                    radioButton21.Checked = true;
                    radioButton20.Checked = false;
                    radioButton19.Checked = false;
                }
                else if (sigara == 2)
                {
                    radioButton21.Checked = false;
                    radioButton20.Checked = true;
                    radioButton19.Checked = false;
                }
                else if (sigara == 3)
                {
                    radioButton21.Checked = false;
                    radioButton20.Checked = false;
                    radioButton19.Checked = true;
                }

                if (alkol == 1)
                {
                    radioButton30.Checked = true;
                    radioButton29.Checked = false;
                    radioButton22.Checked = false;
                }
                else if (alkol == 2)
                {
                    radioButton30.Checked = false;
                    radioButton29.Checked = true;
                    radioButton22.Checked = false;
                }
                else if (alkol == 3)
                {
                    radioButton30.Checked = false;
                    radioButton29.Checked = false;
                    radioButton22.Checked = true;


                }
                sqlCon.Close();
                SqlCommand kanGrubuCek = new SqlCommand("select * from kanGruplari where kanId=" + kanGrubu, sqlCon);
                sqlCon.Open();
                SqlDataReader kanRdr = kanGrubuCek.ExecuteReader(CommandBehavior.CloseConnection);
                while (kanRdr.Read())
                {

                    comboBox16.Text = kanRdr.GetString(1);

                }

                sqlCon.Close();
                SqlCommand sosyalCek = new SqlCommand("select * from sgkDurum where sgkId=" + sosyal, sqlCon);
                sqlCon.Open();
                SqlDataReader sosyalRdr = sosyalCek.ExecuteReader(CommandBehavior.CloseConnection);
                while (sosyalRdr.Read())
                {

                    comboBox13.Text = sosyalRdr.GetString(1);

                }
                sqlCon.Close();


                tabControl1.SelectedTab = tabPage5;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {



            sqlCon.Open();
            SqlCommand muayeneKayit = new SqlCommand("insert into muayene(mKayitNo,tc, aSikayet, aHikayesi, aPsikolojik, aNutrisyon, aFonksiyonel, aAgri,fmVitalBulg,fmBasBoyun,fmKalp,fmSolunumSis,fmBatin,fmEkstremite,iTeshis,iTetkik,iTedaviOneri,iKlinik, oGecirdigiHast, oGecirdigiAml, oAlerji, oSurekliKulIlaclar, oSoyGecmis) values ('" + int.Parse(textBox35.Text) + "','" + textBox36.Text + "','" + textBox45.Text + "', '" + textBox46.Text + "', '" + textBox47.Text + "','" + textBox48.Text + "','" + textBox49.Text + "','" + textBox50.Text + "','" + textBox51.Text + "','" + textBox52.Text + "', '" + textBox53.Text + "','" + textBox54.Text + "','" + textBox55.Text + "','" + textBox56.Text + "','" + textBox57.Text + "','" + textBox58.Text + "', '" + textBox59.Text + "','" + textBox60.Text + "','" + textBox40.Text + "','" + textBox41.Text + "','" + textBox42.Text + "','" + textBox43.Text + "','" + textBox44.Text + "')", sqlCon);

            if (muayeneKayit.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Kayıt başarı ile tamamlandı.");
            }
            else
            {
                MessageBox.Show("Bir sorun oluştu.");
            }

            sqlCon.Close();




        }

        private void button15_Click_1(object sender, EventArgs e)
        {


        }

        private void recete_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(listBox1.SelectedItem);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
        }

        private void label95_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabPage1.Show();
            textBoxClear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tabPage1.Show();
            textBoxClear();
        }

        private void comboBox20_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}