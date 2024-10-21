using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app1
{
    public partial class anasayfa : Form
    {
        public anasayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ogrencikontrol ogrencikontrol = new ogrencikontrol();
            ogrencikontrol.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Yenigiriş formunu aç
            Yenigiriş yenigirisForm = new Yenigiriş();
            yenigirisForm.Show(); // Yenigiriş formunu göster

            this.Hide(); // Form1'i gizle
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Raporlama rapor=new Raporlama();
            rapor.Show();
            this.Hide();
        }
    }
}
