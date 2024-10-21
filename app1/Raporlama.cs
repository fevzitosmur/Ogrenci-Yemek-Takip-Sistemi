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
    public partial class Raporlama : Form
    {
        public Raporlama()
        {
            InitializeComponent();
        }

        private void btnGunlukRapor_Click(object sender, EventArgs e)
        {
            try
            {
                // Veritabanı bağlantısı
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=YemekTakipDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Günlük yemek raporunu çekmek için sorgu
                    string query = @"
                SELECT o.Ad, o.Soyad, k.Barkod, k.YemekZamani
                FROM YemekKontrol k
                JOIN Ogrenciler o ON k.Barkod = o.Barkod
                WHERE CAST(k.YemekZamani AS DATE) = CAST(GETDATE() AS DATE)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt); // Sorgu sonucunu DataTable'a doldur

                            // DataGridView'i doldur
                            dataGridView1.DataSource = dt; // dataGridView1, tasarımda eklediğiniz DataGridView'in ismi
                        }
                    }

                    // Günlük yemek sayısını hesaplayıp label'a yaz
                    string countQuery = "SELECT COUNT(DISTINCT k.Barkod) AS GunlukYemekSayisi FROM YemekKontrol k WHERE CAST(k.YemekZamani AS DATE) = CAST(GETDATE() AS DATE)";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, connection))
                    {
                        object result = countCommand.ExecuteScalar();
                        int gunlukYemekSayisi = Convert.ToInt32(result);
                        labelGunlukYemekSayisi.Text = $"Bugün yemek yiyen kişi sayısı: {gunlukYemekSayisi}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}");
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Veritabanı bağlantısı
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=YemekTakipDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Haftalık yemek raporunu çekmek için sorgu
                    string query = @"
        SELECT o.Ad, o.Soyad, k.Barkod, k.YemekZamani
        FROM YemekKontrol k
        JOIN Ogrenciler o ON k.Barkod = o.Barkod
        WHERE k.YemekZamani >= DATEADD(DAY, -7, GETDATE())";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt); // Sorgu sonucunu DataTable'a doldur

                            // DataGridView'i doldur
                            dataGridView1.DataSource = dt; // dataGridView1, tasarımda eklediğiniz DataGridView'in ismi
                        }
                    }

                    // Haftalık yemek sayısını hesaplayıp label'a yaz
                    string countQuery = "SELECT COUNT(DISTINCT k.Barkod) AS HaftalikYemekSayisi FROM YemekKontrol k WHERE k.YemekZamani >= DATEADD(DAY, -7, GETDATE())";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, connection))
                    {
                        object result = countCommand.ExecuteScalar();
                        int haftalikYemekSayisi = Convert.ToInt32(result);
                        labelGunlukYemekSayisi.Text = $"Bu hafta yemek yiyen kişi sayısı: {haftalikYemekSayisi}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Veritabanı bağlantısı
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=YemekTakipDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Aylık yemek raporunu çekmek için sorgu
                    string query = @"
        SELECT o.Ad, o.Soyad, k.Barkod, k.YemekZamani
        FROM YemekKontrol k
        JOIN Ogrenciler o ON k.Barkod = o.Barkod
        WHERE k.YemekZamani >= DATEADD(DAY, -30, GETDATE())";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt); // Sorgu sonucunu DataTable'a doldur

                            // DataGridView'i doldur
                            dataGridView1.DataSource = dt; // dataGridView1, tasarımda eklediğiniz DataGridView'in ismi
                        }
                    }

                    // Aylık yemek sayısını hesaplayıp label'a yaz
                    string countQuery = "SELECT COUNT(DISTINCT k.Barkod) AS AylikYemekSayisi FROM YemekKontrol k WHERE k.YemekZamani >= DATEADD(DAY, -30, GETDATE())";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, connection))
                    {
                        object result = countCommand.ExecuteScalar();
                        int aylikYemekSayisi = Convert.ToInt32(result);
                        labelGunlukYemekSayisi.Text = $"Bu ay yemek yiyen kişi sayısı: {aylikYemekSayisi}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Veritabanı bağlantısı
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=YemekTakipDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Yıllık yemek raporunu çekmek için sorgu
                    string query = @"
        SELECT o.Ad, o.Soyad, k.Barkod, k.YemekZamani
        FROM YemekKontrol k
        JOIN Ogrenciler o ON k.Barkod = o.Barkod
        WHERE k.YemekZamani >= DATEADD(YEAR, -1, GETDATE())";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt); // Sorgu sonucunu DataTable'a doldur

                            // DataGridView'i doldur
                            dataGridView1.DataSource = dt; // dataGridView1, tasarımda eklediğiniz DataGridView'in ismi
                        }
                    }

                    // Yıllık yemek sayısını hesaplayıp label'a yaz
                    string countQuery = "SELECT COUNT(DISTINCT k.Barkod) AS YillikYemekSayisi FROM YemekKontrol k WHERE k.YemekZamani >= DATEADD(YEAR, -1, GETDATE())";
                    using (SqlCommand countCommand = new SqlCommand(countQuery, connection))
                    {
                        object result = countCommand.ExecuteScalar();
                        int yillikYemekSayisi = Convert.ToInt32(result);
                        labelGunlukYemekSayisi.Text = $"Bu yıl yemek yiyen kişi sayısı: {yillikYemekSayisi}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}");
            }
        }
    }
}
