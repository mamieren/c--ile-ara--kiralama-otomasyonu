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

namespace arabakiralama
{
    public partial class LoginForm : Form
    {
        SqlConnection con = new SqlConnection("server=DESKTOP-3M4E4P0;initial catalog=otoKiraDB;integrated security =sspi");
        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string kullanici = txtUsername.Text.Trim();
            string sifre = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(kullanici) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz.");
                return;
            }

            string query = "SELECT COUNT(*) FROM Kullanicilar WHERE kullaniciAdi=@kullanici AND sifre=@sifre";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@kullanici", kullanici);
            cmd.Parameters.AddWithValue("@sifre", sifre);

            con.Open();
            int result = (int)cmd.ExecuteScalar();
            con.Close();

            if (result > 0)
            {
                MessageBox.Show("Giriş başarılı!");

                this.Hide();
                Form1 form = new Form1();
                form.FormClosed += (s, args) => this.Close();
                form.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
            }

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string kullanici = txtUsername.Text.Trim();
            string sifre = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(kullanici) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre belirleyin.");
                return;
            }

            string kontrolQuery = "SELECT COUNT(*) FROM Kullanicilar WHERE kullaniciAdi=@kullanici";
            SqlCommand kontrolCmd = new SqlCommand(kontrolQuery, con);
            kontrolCmd.Parameters.AddWithValue("@kullanici", kullanici);

            con.Open();
            int varMi = (int)kontrolCmd.ExecuteScalar();

            if (varMi > 0)
            {
                MessageBox.Show("Bu kullanıcı adı zaten alınmış.");
                con.Close();
                return;
            }

            string insertQuery = "INSERT INTO Kullanicilar (kullaniciAdi, sifre) VALUES (@kullanici, @sifre)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, con);
            insertCmd.Parameters.AddWithValue("@kullanici", kullanici);
            insertCmd.Parameters.AddWithValue("@sifre", sifre);
            insertCmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Kayıt başarılı! Artık giriş yapabilirsiniz.");
        }
    }
}
