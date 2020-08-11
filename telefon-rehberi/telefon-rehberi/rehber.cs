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
    public partial class rehber : Form
    {
        crud c = new crud();
        public rehber()
        {
            InitializeComponent();
        }
        void verigetir()
        {
            DataSet ds = c.GetDataSet("select * from rehber where gkuladi= '" + giris.kuladi + "' ");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            dataGridView1.Columns["radi"].HeaderText = "Ad";
            dataGridView1.Columns["rsoyadi"].HeaderText = "Soyad";
            dataGridView1.Columns["radres"].HeaderText = "Adres";
            dataGridView1.Columns["ril"].HeaderText = "İl";
            dataGridView1.Columns["rilce"].HeaderText = "İlçe";
            dataGridView1.Columns["rceptel"].HeaderText = "Cep Telefonu";
            dataGridView1.Columns["revtel"].HeaderText = "Ev Telefonu";
            dataGridView1.Columns["ristel"].HeaderText = "İş Telefonu";
            dataGridView1.Columns["rdogumtarihi"].HeaderText = "Doğum Tarihi";
            dataGridView1.Columns["raciklama"].HeaderText = "Açıklama";
            dataGridView1.Columns["rid"].Visible = false;
            dataGridView1.Columns["rkayittarihi"].Visible = false;
            dataGridView1.Columns["rguncellemetarihi"].Visible = false;
            dataGridView1.Columns["gkuladi"].Visible = false;
            dataGridView1.ClearSelection();

            DataTable dt = c.GetDataTable("select * from iller ORDER BY id ASC ");
            txtIl.ValueMember = "id";
            txtIl.DisplayMember = "sehir";
            txtIl.DataSource = dt;

            txtIl.DropDownStyle = ComboBoxStyle.DropDownList;
            txtIlce.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAd.Text = dataGridView1.CurrentRow.Cells["radi"].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells["rsoyadi"].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells["radres"].Value.ToString();
            txtIl.Text = dataGridView1.CurrentRow.Cells["ril"].Value.ToString();
            txtIlce.Text = dataGridView1.CurrentRow.Cells["rilce"].Value.ToString();
            txtCep.Text = dataGridView1.CurrentRow.Cells["rceptel"].Value.ToString();
            txtEv.Text = dataGridView1.CurrentRow.Cells["revtel"].Value.ToString();
            txtIs.Text = dataGridView1.CurrentRow.Cells["ristel"].Value.ToString();
            txtDogum.Text = dataGridView1.CurrentRow.Cells["rdogumtarihi"].Value.ToString();
            txtAciklama.Text = dataGridView1.CurrentRow.Cells["raciklama"].Value.ToString();
        }
        void temizle()
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtAdres.Text = "";
            txtIl.Text = "";
            txtIlce.Text = "";
            txtEv.Text = "";
            txtIs.Text = "";
            txtCep.Text = "";
            txtAciklama.Text = "";
            txtDogum.Text = "";
            dataGridView1.ClearSelection();
        }
        private void rehber_Load(object sender, EventArgs e)
        {
            verigetir();
            temizle();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtAdres.Text == "" || txtIl.Text == "" || txtIlce.Text == "" || txtCep.Text == "" || txtEv.Text == "" || txtIl.Text == "")
            {
                MessageBox.Show("Alanlar boş geçilemez!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtIs.Text.Length != 14)
                {
                    MessageBox.Show("İş telefon numarası 10 haneli olmalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (txtCep.Text.Length != 14)
                    {
                        MessageBox.Show("Cep telefon numarası 10 haneli olmalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (txtEv.Text.Length != 8)
                        {
                            MessageBox.Show("Ev telefon numarası 7 haneli olmalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            c.cmd("insert into rehber (radi, rsoyadi, radres, ril, rilce, rceptel, revtel, ristel, rdogumtarihi, raciklama, rkayittarihi, gkuladi) values ('" + txtAd.Text + "','" + txtSoyad.Text + "','" + txtAdres.Text + "','" + txtIl.Text + "','" + txtIlce.Text + "','" + txtCep.Text + "','" + txtEv.Text + "','" + txtIs.Text + "','" + txtDogum.Text + "','" + txtAciklama.Text + "','" + DateTime.Now + "', '" + giris.kuladi + "')");
                            verigetir();
                            temizle();
                        }
                    }
                }
            }
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                    c.cmd("delete from rehber where rid = " + dataGridView1.CurrentRow.Cells["rid"].Value + "");
                    verigetir();
                    temizle();

            }
            else
            {
                MessageBox.Show("Silmek istediğini kaydı seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtAdres.Text == "" || txtIl.Text == "" || txtIlce.Text == "" || txtCep.Text == "" || txtEv.Text == "" || txtIl.Text == "")
            {
                MessageBox.Show("Güncellemek istediğini kaydı seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    c.cmd("update rehber set radi='" + txtAd.Text + "', rsoyadi='" + txtSoyad.Text + "', radres='" + txtAdres.Text + "', ril='" + txtIl.Text + "',rilce='" + txtIlce.Text + "', rceptel='" + txtCep.Text + "',revtel='" + txtEv.Text + "', ristel= '" + txtIs.Text + "', rdogumtarihi='" + txtDogum.Text + "', raciklama='" + txtAciklama.Text + "', rguncellemetarihi='" + DateTime.Now + "' where rid = " + dataGridView1.CurrentRow.Cells["rid"].Value + "");
                    verigetir();
                    temizle();
                }
                else
                {
                    MessageBox.Show("Listeden seçim yapın!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            giris g = new giris();
            g.Show();
            this.Hide();
        }
        private void txtIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtIl.SelectedIndex != -1)
            {
                DataTable dt = c.GetDataTable("select * from ilceler where sehir=" + txtIl.SelectedValue + "");
                txtIlce.ValueMember = "id";
                txtIlce.DisplayMember = "ilce";
                txtIlce.DataSource = dt;
            }
        }
        private void txtAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }
        private void txtSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }
    }
}
