using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using MySql.Data.MySqlClient;

namespace arabakiralama
{
  
    public partial class Form1 : Form
    {
        

        SqlConnection con = new SqlConnection("server=DESKTOP-3M4E4P0;initial catalog=otoKiraDB;integrated security =sspi");
        SqlCommand cmd;
        SqlDataAdapter da;

        void temizle()
        {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            comboBox1.Text = "";
            pictureBox1.ImageLocation = "";
        }





        void listele()
        {
            da = new SqlDataAdapter("select *from arabalar", con);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            temizle();
        }

        public Form1()
        {

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" ||
       textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" ||
       comboBox1.Text == "" || pictureBox1.ImageLocation == "")
            {
                MessageBox.Show("Lütfen tüm alanlarý doldurunuz!");
                return;
            }

            cmd = new SqlCommand("INSERT INTO arabalar (plaka, marka, model, uretimYili, km, renk, yakitTuru, kiraUcreti, durum, resim) " +
          "VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', " + int.Parse(textBox4.Text) + ", " +
          int.Parse(textBox5.Text) + ", '" + textBox6.Text + "', '" + textBox7.Text + "', " + int.Parse(textBox8.Text) + ", '" +
          comboBox1.Text + "', '" + pictureBox1.ImageLocation + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayýt Eklendi");
            listele();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofl = new OpenFileDialog();
            ofl.Filter = "Resim Dosyalarý |*.jpg;*.png;*.tif| Tum dosyalar|*.*   ";
            ofl.ShowDialog();
            pictureBox1.ImageLocation = ofl.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" ||
        textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" ||
        comboBox1.Text == "" || pictureBox1.ImageLocation == "")
            {
                MessageBox.Show("Lütfen güncellemek için kayýt seçin!");
                return;
            }


            cmd = new SqlCommand("UPDATE arabalar set plaka ='" + textBox1.Text + "',marka= '" + textBox2.Text + "', model= '" + textBox3.Text + "',uretimYili= '" + int.Parse(textBox4.Text) + "',km= '" + int.Parse(textBox5.Text) + "',renk= '" + textBox6.Text + "',yakitTuru= '" + textBox7.Text + "', kiraUcreti='" + int.Parse(textBox8.Text) + "',durum= '" +
          comboBox1.Text + "',resim= '" + pictureBox1.ImageLocation + "' where id='" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + "'    ", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayýt Güncellendi");
            listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[10].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silmek için bir kayýt seçiniz!");
                return;
            }


            cmd = new SqlCommand("DELETE from arabalar where id='" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + "'    ", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayýt Silindi");
            listele();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            temizle();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("select *from arabalar WHERE marka like '"+textBox9.Text+"%' ", con);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            temizle();
        }
    }
}
