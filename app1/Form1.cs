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
    public partial class Form1 : Form
    {
        private string correctUsername = "admin";
        private string correctPassword = "12345";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve şifre doğrulama
            if (textBox1.Text == correctUsername && textBox2.Text == correctPassword)
            {
                // Giriş başarılı ise Form2'yi aç
                anasayfa form2 = new anasayfa();
                form2.Show(); // Form2'yi göster

                this.Hide(); // Form1'i gizle
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı adı veya şifre.");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

    }
}
