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
    public partial class Yenigiriş : Form
    {
        public Yenigiriş()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            //Veritabanı bağlantısı
            string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = YemekTakipDB; Integrated Security = True; Encrypt = False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // SQL sorgusu
                string query = "INSERT INTO Ogrenciler (Ad,Soyad,Sinif,Yas,Barkod) VALUES (@Ad, @Soyad, @Sinif, @Yas, @Barkod)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Textboxlardan verileri al
                    command.Parameters.AddWithValue("@Ad", textBox1.Text);
                    command.Parameters.AddWithValue("@Soyad", textBox2.Text);
                    command.Parameters.AddWithValue("@Sinif", textBox3.Text);
                    command.Parameters.AddWithValue("@Yas", int.Parse(textBox4.Text));
                    command.Parameters.AddWithValue("@Barkod", textBox5.Text);

                    // Sorguyu çalıştır
                    command.ExecuteNonQuery();
                }
            }
            // İşlem sonrası bilgilendirme
            MessageBox.Show("Öğrenci başarıyla kaydedildi.");
        }
    }
}
