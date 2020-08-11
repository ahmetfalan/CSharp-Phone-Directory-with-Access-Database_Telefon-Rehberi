using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace telefon_rehberi
{
    public partial class giris : Form
    {
        crud c = new crud();
        public giris()
        {
            InitializeComponent();
        }
        private void üyeOlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Size.Width == 550 && this.Size.Height == 224)
            {
                this.Size = new Size(270, 185);
            }
            else
            {
                this.Size = new Size(550, 224);
            }
        }
        private void şifremiUnuttumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Size.Width == 270 && this.Size.Height == 356)
            {
                this.Size = new Size(270, 185);
            }
            else
            {
                this.Size = new Size(270, 356);
            }
        }
        public static string kuladi = "";
        private void giris_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.kuladi != string.Empty)
            {
                txtGKuladi.Text = Properties.Settings.Default.kuladi;
                txtGSifre.Text = Properties.Settings.Default.sifre;
            }
        }

        public void temizle()
        {
            txtKKuladi.Text = "";
            txtKSifre.Text = "";
            txtSifreK.Text = "";
            txtAnahtar.Text = "";

        }

        private void btnGenislet_Click(object sender, EventArgs e)
        {

        }
        private void btnKayit_Click(object sender, EventArgs e)
        {
            if (txtKSifre.Text == txtSifreK.Text)
            {
                if (txtAnahtar.Text != "" || txtKSifre.Text != "" || txtSifreK.Text != "")
                {
                    OleDbCommand cmd = new OleDbCommand("select count(*) from giris where gkuladi='" + txtKKuladi.Text + "'", c.baglanti());
                    if (cmd.ExecuteScalar().ToString() == "0")
                    {
                        c.cmd("insert into giris (gkuladi, gsifre, gkayittarihi, ganahtar) values ('" + txtKKuladi.Text + "','" + txtKSifre.Text + "', '" + DateTime.Now + "' ,'" + txtAnahtar.Text + "')");
                        temizle();
                        this.Size = new Size(270, 185);
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
        private void btnGiris_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select count(*) from giris where gkuladi='" + txtGKuladi.Text + "' and gsifre='" + txtGSifre.Text + "'", c.baglanti());
            if (cmd.ExecuteScalar().ToString() == "0")
            {
                MessageBox.Show("Giriş bilgilerinizi kontrol edin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                kuladi = txtGKuladi.Text;
                rehber r = new rehber();
                r.Show();
                this.Hide();
            }
            if (checkHatirla.Checked)
            {
                telefon_rehberi.Properties.Settings.Default.kuladi = txtGKuladi.Text;
                telefon_rehberi.Properties.Settings.Default.sifre = txtGSifre.Text;
                telefon_rehberi.Properties.Settings.Default.Save();
            }
            else if (!checkHatirla.Checked)
            {
                telefon_rehberi.Properties.Settings.Default.kuladi = "";
                telefon_rehberi.Properties.Settings.Default.sifre = "";
                telefon_rehberi.Properties.Settings.Default.Save();
            }
        }

        private void btnSifreGoster_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("select count(*) from giris where gkuladi='" + txtSifreKuladi.Text + "' and ganahtar='" + txtSifreAnahtar.Text + "'", c.baglanti());
            if (cmd.ExecuteScalar().ToString() != "0")
            {
                DataRow dr = c.GetDataRow("select * from giris where gkuladi='" + txtSifreKuladi.Text + "' and ganahtar='" + txtSifreAnahtar.Text + "'");
                this.Size = new Size(270, 185);
                MessageBox.Show("Şifreniz: " + dr["ganahtar"].ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kullanıcı adınızı ya da anahtar kelimenizi kontrol edin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pictureBox1, "Şifre yenilemenize yardımcı olacak herhangi bir kelime.");
        }
    }
}
