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

namespace app1
{
    public partial class ogrencikontrol : Form
    {
        public ogrencikontrol()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Eğer "Enter" tuşuna basılmışsa
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Veritabanı bağlantısı
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=YemekTakipDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Barkod bilgisi
                    string barkod = textBox1.Text;

                    // Öğrenci bilgilerini sorgula ve ilgili TextBox'lara getir
                    string queryOgrenci = "SELECT Ad, Soyad, Sinif, Yas, Barkod FROM Ogrenciler WHERE Barkod = @Barkod";
                    using (SqlCommand commandOgrenci = new SqlCommand(queryOgrenci, connection))
                    {
                        commandOgrenci.Parameters.AddWithValue("@Barkod", barkod);

                        using (SqlDataReader reader = commandOgrenci.ExecuteReader())
                        {
                            if (reader.Read()) // Öğrenci bulunduysa bilgilerini doldur
                            {
                                textBox2.Text = reader["Ad"].ToString();
                                textBox3.Text = reader["Soyad"].ToString();
                                textBox4.Text = reader["Sinif"].ToString();
                                textBox5.Text = reader["Yas"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Öğrenci bulunamadı.");
                                return; // Öğrenci bulunamadıysa işlemi sonlandır
                            }
                        }
                    }

                    // Öğrencinin en son yemek yediği zamanı sorgula
                    string queryYemek = "SELECT TOP 1 YemekZamani FROM YemekKontrol WHERE Barkod = @Barkod ORDER BY YemekZamani DESC";

                    using (SqlCommand commandYemek = new SqlCommand(queryYemek, connection))
                    {
                        commandYemek.Parameters.AddWithValue("@Barkod", barkod);
                        object result = commandYemek.ExecuteScalar();  // En son yemek zamanını al

                        if (result != null)
                        {
                            DateTime sonYemekZamani = (DateTime)result;
                            TimeSpan fark = DateTime.Now - sonYemekZamani;

                            if (fark.TotalHours >= 10)
                            {
                                // 10 saatten fazla geçmişse "Afiyet olsun" yaz ve yeni yemek kaydını ekle
                                MessageBox.Show("Afiyet olsun.");

                                // Yeni yemek yeme zamanını kaydet
                                string insertQuery = "INSERT INTO YemekKontrol (Barkod, YemekZamani) VALUES (@Barkod, @YemekZamani)";
                                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@Barkod", barkod);
                                    insertCommand.Parameters.AddWithValue("@YemekZamani", DateTime.Now);
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // 10 saatten az geçmişse "Öğrenci Günlük Yemek hakkını kullandı" yaz
                                MessageBox.Show("Öğrenci Günlük Yemek hakkını kullandı.");
                            }
                        }
                        else
                        {
                            // Öğrencinin yemek kaydı yoksa ilk defa yiyor demektir
                            MessageBox.Show("Afiyet olsun.");

                            // İlk yemek zamanını kaydet
                            string insertQuery = "INSERT INTO YemekKontrol (Barkod, YemekZamani) VALUES (@Barkod, @YemekZamani)";
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@Barkod", barkod);
                                insertCommand.Parameters.AddWithValue("@YemekZamani", DateTime.Now);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                // Barkod alanını temizleyelim ki yeni barkod girilsin
                textBox1.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Anasayfa formunu aç ve konumunu Form1'in konuma ayarla
            anasayfa anasayfaform = new anasayfa();

            // Yeni formun başlangıç pozisyonunu manuel yapıyoruz

            anasayfaform.StartPosition = FormStartPosition.Manual;

            // Form1'in pozisyonunu yeni forma aktarıyoruz.
            anasayfaform.Location = this.Location;

            //Yeni Formu göster
            anasayfaform.Show();
            this.Hide();
        }
    }
}
