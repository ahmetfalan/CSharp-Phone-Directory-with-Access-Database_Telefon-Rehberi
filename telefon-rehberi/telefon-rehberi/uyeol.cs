using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace telefon_rehberi
{
    public partial class uyeol : Form
    {
        crud c = new crud();
        public uyeol()
        {
            InitializeComponent();
        }
        public void temizle()
        {
            txtKuladi.Text = "";
            txtAnahtar.Text = "";
            txtSifre.Text = "";
            txtSifreK.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSifre.Text == txtSifreK.Text)
            {
                if (txtAnahtar.Text != "" || txtKuladi.Text != "" || txtSifre.Text != "")
                {
                    OleDbCommand cmd = new OleDbCommand("select count(*) from giris where gkuladi='" + txtKuladi.Text + "'", c.baglanti());
                    if (cmd.ExecuteScalar().ToString() == "0")
                    {
                        c.cmd("insert into giris (gkuladi, gsifre, gkayittarihi, ganahtar) values ('" + txtKuladi.Text + "','" + txtSifre.Text + "', '" + DateTime.Now + "' ,'" + txtAnahtar.Text + "')");
                        temizle();
                        MessageBox.Show("Kayıt başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Bu kullanıcı adı alındı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Alanları doldurunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Şifreler uyuşmuyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            giris g = new giris();
            g.Show();
            this.Hide();
        }

        private void uyeol_Load(object sender, EventArgs e)
        {

        }
    }
}
